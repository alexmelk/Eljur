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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace Eljur.Controllers
{
    [Authorize(Roles = "admin, teacher")]
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

            var tableColumns = Convert.ToInt32(input.Time / 2);
            var output = new List<VisitGroupForColumnModel>() { };
            for (int i = 0; i < tableColumns; i++)
            {
                var visits = new List<VisitModify>();
                for (int j = 0; j < input.Group.Students.Count(); j++) visits.Add(new VisitModify());

                output.Add(new VisitGroupForColumnModel()
                {
                    VisitsModify = visits
                });
            }

            var model = new VisitViewModel() { Input = input, Output = output, Date = DateTime.Now };

            return View("VisitsView", model);
        }
        /// <summary>
        /// Добавление посещений
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IActionResult AddVisit(VisitViewModel model)
        {
            var date = model.Date;
            foreach (var column in model.Output)
            {
                var theme = _db.Theme.Find(column.ThemeId);
                var typeSubject = column.TypeSubject;

                var visit = new GroupVisit()
                {
                    Date = date,
                    TypeSubject = typeSubject,
                    Theme = theme,
                    Subject = _db.Subject.Find(model.SubjectId),
                    Group = _db.Group.Find(model.GroupId),
                };
                _db.GroupVisit.Add(visit);


                foreach (var visitModify in column.VisitsModify)
                {
                    var studentVisit = new StudentVisit()
                    {
                        TypeVisit = visitModify.TypeVisit,
                        Student = _db.Student.Find(visitModify.StudentId),
                        GroupVisit = visit,
                        Subject = _db.Subject.Find(model.SubjectId),
                    };
                    _db.StudentVisit.Add(studentVisit);

                }

                _db.SaveChanges();

            }

            _db.SaveChanges();

            return View("VisitAddedView");
        }
        /// <summary>
        /// Редактирование посещения
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IActionResult EditGroupVisit(ChoosePropertyVisit model)
        {
            model.Group = _db.Group.Find(model.Group.Id);
            model.Subject = _db.Subject.Find(model.Subject.Id);

            var visits = _db.GroupVisit
                .Include(x => x.StudentVisits)
                .ThenInclude(x => x.Student)
                .Include(x => x.Subject)
                .Include(x => x.Theme)
                .Where(x => ((x.Subject.Id == model.Subject.Id) && (x.StudentVisits.FirstOrDefault().Student.Group.Id == model.Group.Id))).ToList();

            return View("VisitTableView", visits);
        }
        public IActionResult EditStudentVisitView(int id)
        {
            var model = _db.StudentVisit
                .Include(x => x.Student)
                .Include(x => x.GroupVisit)
                .ThenInclude(x => x.Subject)
                .Include(x => x.GroupVisit)
                .ThenInclude(x => x.Group)
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return View("EditVisitView", model);
        }
        [HttpPost]
        public IActionResult EditStudentVisitView(StudentVisit model, int subjectId, int groupId)
        {
            var visit = _db.StudentVisit.Find(model.Id);
            visit.TypeVisit = model.TypeVisit;
            _db.SaveChanges();

            var visits = _db.GroupVisit
                .Include(x => x.StudentVisits)
                .ThenInclude(x => x.Student)
                .Include(x => x.Subject)
                .Include(x => x.Group)
                .Include(x => x.Theme)
                .Where(x => ((x.Subject.Id == subjectId)
                        && (x.StudentVisits.FirstOrDefault().Student.Group.Id == groupId)))
                .ToList();

            return View("VisitTableView", visits);
        }
        /// <summary>
        /// Удалить посещение
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult RemoveStudentVisit(int id, int groupId, int subjectId)
        {
            var visit = _db.StudentVisit.Find(id);
            _db.StudentVisit.Remove(visit);
            _db.SaveChanges();

            var visits = _db.GroupVisit
                             .Include(x => x.StudentVisits)
                             .ThenInclude(x => x.Student)
                             .Include(x => x.Subject)
                             .Include(x => x.Group)
                             .Include(x => x.Theme)
                             .Where(x => ((x.Subject.Id == subjectId)
                             && (x.StudentVisits.FirstOrDefault().Student.Group.Id == groupId)))
                             .ToList();

            return View("VisitTableView", visits);
        }
        /// <summary>
        /// Отображаем настройку "Посещения"
        /// </summary>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public IActionResult VisitView(ChoosePropertyVisit model, string action)
        {
            model.Group = _db.Group.Find(model.Group.Id);
            model.Subject = _db.Subject.Find(model.Subject.Id);

            var group = _db.Group.Include(x => x.GroupVisits).Include(x => x.Subjects).Where(x => x.Id == model.Group.Id).FirstOrDefault();
            var subject = group.Subjects.Where(x => x.Id == model.Subject.Id).ToList();
            if (subject.Count() == 0)
            {
                return View("NoVisits", model);
            }

            switch (action)
            {
                case "create": { return GetExcel(model); }
                case "edit": { return EditGroupVisit(model); }
            }
            return View("VisitView");
        }
        /// <summary>
        /// Создаём excel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IActionResult GetExcel(ChoosePropertyVisit model)
        {
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
                    .ThenInclude(x => x.StudentVisits)
                    .ThenInclude(x => x.GroupVisit)
                    .ThenInclude(x => x.Subject)
                    .Include(x => x.Students)
                    .ThenInclude(x => x.StudentVisits)
                    .ThenInclude(x => x.GroupVisit)
                    .ThenInclude(x => x.Group)
                    .Include(x => x.GroupVisits)
                    .ThenInclude(x => x.Theme)
                    .Include(x => x.Subjects)
                    .Include(x => x.GroupVisits)
                    .Where(x => x.Id == model.Group.Id)
                    .FirstOrDefault();

                var students = group.Students.OrderBy(x => x.FIO).ToList();
                //посещаемость студентов
                for (int i = 0; i < students.Count(); i++)
                {
                    var visits = students[i].StudentVisits
                        .Where(x => x.GroupVisit.Subject.Id == model.Subject.Id)
                        .Where(x=>x.GroupVisit.Group.Id==model.Group.Id)
                        .ToList();

                    WriteStudentsVisits(visits, package, i);
                }

                //название предмета
                var subjectName = package.Workbook.Names[$"SubjectName"];
                package.Workbook.Worksheets["Лист1"].Cells[subjectName.Address].Value = model.Subject.Name;

                //учёт занятий
                var groupVisits = group.GroupVisits.Where(x => x.Subject.Id == model.Subject.Id).ToList();
                for (int i = 0; i < groupVisits.Count(); i++)
                {
                    var data = groupVisits[i];
                    var date = data.Date.ToString("dd.MM.yyyy");
                    var datePlace = package.Workbook.Names[$"time{i + 1}"]; //left
                    package.Workbook.Worksheets["Лист1"].Cells[datePlace.Address].Value = date;

                    var datePlace1 = package.Workbook.Names[$"date_{i + 1}"]; //right
                    package.Workbook.Worksheets["Лист1"].Cells[datePlace1.Address].Value = date;

                    var visitType = package.Workbook.Names[$"visitType{i + 1}"];
                    package.Workbook.Worksheets["Лист1"].Cells[visitType.Address].Value = (TypeSubjectRusEnum)data.TypeSubject;

                    var themeName = package.Workbook.Names[$"themeName{i + 1}"];
                    package.Workbook.Worksheets["Лист1"].Cells[themeName.Address].Value = data.Theme.Name;

                    var hour = package.Workbook.Names[$"hour"];
                    package.Workbook.Worksheets["Лист1"].Cells[hour.Address].Value = 2;

                }

                package.SaveAs(stream);
            }

            stream.Position = 0;
            return File(stream, "application/xlsx", $"Посещаемость[{model.Group.Name} - {model.Subject.Name}].xlsx");
        }
        public void WriteStudentsVisits(List<StudentVisit> visits, ExcelPackage package, int index)
        {
            int validHours = 0;
            int inValidHours = 0;
            var list = new List<string>();

            foreach (var visit in visits)
            {
                switch (visit.TypeVisit)
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
