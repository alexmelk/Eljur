using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Eljur.Context;
using Eljur.Context.Tables;
using Eljur.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace Eljur.Controllers
{
    public class VisitController : Controller
    {
        dbContext _db;
        public VisitController(dbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        //views
        public IActionResult ChooseGroupView()
        {
            var model = new ChoosePropertyVisit();
            return View(model);
        }
        public IActionResult ChooseSubjectView()
        {
            return View();
        }
        public IActionResult ChooseTimeView()
        {
            return View();
        }
        public IActionResult NoVisits(ChoosePropertyVisit model)
        {
            return View(model);
        }

        public IActionResult ChooseGroup(ChoosePropertyVisit model)
        {
            return View("ChooseSubjectView", model);
        }
        public IActionResult ChooseSubject(ChoosePropertyVisit model)
        {
            return View("ChooseTimeView", model);
        }
        public IActionResult ChooseTime(ChoosePropertyVisit input)
        {
            input.Group = _db.Group.Include(x => x.Students).Where(x => x.Id == input.Group.Id).FirstOrDefault();
            input.Subject = _db.Subject.Include(x => x.Themes).Where(x => x.Id == input.Subject.Id).FirstOrDefault();

            var events = Convert.ToInt32(input.Time / 2);
            var output = new List<VisitGroupForColumnModel>() { };
            for (int i = 0; i < events; i++)
            {
                var visits = new List<VisitModify>();
                for (int j = 0; j < input.Group.Students.Count(); j++) visits.Add(new VisitModify());

                output.Add(new VisitGroupForColumnModel() { 
                    VisitsModify = visits
                });
            }

            var model = new VisitViewModel() { Input = input, Output = output, Date = DateTime.Now};

            return View("VisitsView", model);
        }

        public IActionResult AddVisit(VisitViewModel model)
        {
            List<Visit> visits = new List<Visit>();
            var date = model.Date;
            foreach(var column in model.Output)
            {
                var theme = _db.Theme.Find(column.ThemeId);
                var typeSubject = column.TypeSubject;

                foreach(var visitModify in column.VisitsModify)
                {
                    var visit = new Visit()
                    {
                        Date = date,
                        TypeSubject = typeSubject,
                        TypeVisit = visitModify.TypeVisit,
                        Student = _db.Student.Find(visitModify.StudentId),
                        Theme = theme,
                        Subject = _db.Subject.Find(model.SubjectId),
                        
                    };
                    visits.Add(visit);
                }
            }

            _db.Visit.AddRange(visits);
            _db.SaveChanges();

            return View("Index");
        }


        public IActionResult GetExcel(ChoosePropertyVisit model)
        {
            model.Group = _db.Group.Find(model.Group.Id);
            model.Subject = _db.Subject.Find(model.Subject.Id);

            var group = _db.Group.Include(x => x.Visits).Include(x=>x.Subjects).Where(x => x.Id == model.Group.Id).FirstOrDefault();
            var subject = group.Subjects.Where(x => x.Id == model.Subject.Id).ToList();
            if (subject.Count() == 0)
            {
                return View("NoVisits", model);
            }
                 
            return GenerateExcel(model);
        }

        public FileResult GenerateExcel(ChoosePropertyVisit model)
        {
            Stream stream = new FileStream(path: ".//file.xlsx", FileMode.Create);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(".//template.xlsx")))
            {
                var group = _db.Group
                    .Include(x => x.Students)
                    .ThenInclude(x=>x.Visits)
                    .ThenInclude(x => x.Theme)
                    .Include(x => x.Subjects)
                    .Include(x => x.Visits)
                    .Where(x => x.Id == model.Group.Id)
                    .FirstOrDefault();

                //посещаемость студентов
                for (int i = 0; i < group.Students.Count();i++)
                {
                    var visits = group.Students[i].Visits.Where(x => x.Subject.Id == model.Subject.Id).ToList();

                    WriteStudentsVisits(visits, package, i);
                }

                //название предмета
                var subjectName = package.Workbook.Names[$"SubjectName"];
                package.Workbook.Worksheets["Лист1"].Cells[subjectName.Address].Value = model.Subject.Name;

                //дата
                var firstStudent = group.Students.FirstOrDefault();
                var visitsForSubject = firstStudent.Visits.Where(x => x.Subject.Id == model.Subject.Id).ToList();

                for (int i = 0; i < visitsForSubject.Count(); i++)
                {
                    var data = visitsForSubject[i];
                    var date = data.Date.ToString("dd.MM.yyyy");
                    var datePlace = package.Workbook.Names[$"time{i+1}"]; //left
                    package.Workbook.Worksheets["Лист1"].Cells[datePlace.Address].Value = date;

                    var datePlace1 = package.Workbook.Names[$"date_{i+1}"]; //right
                    package.Workbook.Worksheets["Лист1"].Cells[datePlace1.Address].Value = date;

                    var visitType = package.Workbook.Names[$"visitType{i+1}"];
                    package.Workbook.Worksheets["Лист1"].Cells[visitType.Address].Value = (TypeSubjectRusEnum)data.TypeSubject;

                    var themeName = package.Workbook.Names[$"themeName{i+1}"];
                    package.Workbook.Worksheets["Лист1"].Cells[themeName.Address].Value = data.Theme.Name;

                    var hour = package.Workbook.Names[$"hour"];
                    package.Workbook.Worksheets["Лист1"].Cells[hour.Address].Value = 2;

                }


                package.SaveAs(stream);
            }

            stream.Position = 0;
            return File(stream, "application/xlsx", $"Посещаемость[{model.Group.Name} - {model.Subject.Name}].xlsx");
        }

        public void WriteStudentsVisits(List<Visit> visits, ExcelPackage package, int index)
        {
            int validHours = 0;
            int inValidHours = 0;
            var list = new List<string>();

            foreach(var visit in visits)
            {
                switch(visit.TypeVisit)
                {
                    case TypeVisitEnum.Absent:
                        {
                            list.Add("н");
                            inValidHours += 2;
                            break;
                        }
                    case TypeVisitEnum.ValidAbsent:
                        {
                            list.Add("н");
                            validHours += 2;
                            break;
                        }
                    case TypeVisitEnum.Present:
                        {
                            list.Add("");
                            break;
                        }
                }

            }

            var visitsMas = new object[1, 28];
            for (int i = 0; i < list.Count(); i++)
            {
                visitsMas[0, i] = list[i];
            }

            var student = package.Workbook.Names[$"Student{index + 1}"];
            package.Workbook.Worksheets["Лист1"].Cells[student.Address].Value = visitsMas;
            var valid = package.Workbook.Names[$"valid{index + 1}"];
            package.Workbook.Worksheets["Лист1"].Cells[valid.Address].Value = validHours;
            var inValid = package.Workbook.Names[$"invalid{index + 1}"];
            package.Workbook.Worksheets["Лист1"].Cells[inValid.Address].Value = inValidHours;



        }

    }
}
