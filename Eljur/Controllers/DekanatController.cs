using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eljur.Context;
using Eljur.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eljur.Controllers
{
    public class DekanatController : Controller
    {
        public dbContext _db;

        public DekanatController(dbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult ChooseGroupForSpecialyMark()
        {
            return View(new SpecialyMarkModel() {Semester = new Context.Tables.Semester() });
        }

        [HttpPost]
        public IActionResult ChooseGroupForSpecialyMark(SpecialyMarkModel model)
        {

            var semesterId = model.Semester.Id;
            var semester = _db.Semesters.Include(x => x.SemesterStudents).Include(x=>x.Group).ThenInclude(x=>x.Students).Where(x => x.Id == semesterId).FirstOrDefault();
            var students = semester.Group.Students.OrderBy(x => x.FIO).ToList();
            model.Students = students;

            if (semester.SemesterStudents.ToList().Count() == 0)
            {
                model.SpecialyMark = new List<string>();
                foreach (var student in students)
                {
                    model.SpecialyMark.Add("");
                }
            }
            else
            {
                model.SpecialyMark = semester.SemesterStudents.Select(x => x.SpecialyMark).ToList();
            }

            return View("SpecialyMarkView", model);
        }

        [HttpPost]
        public IActionResult EditSpecialyMark(SpecialyMarkModel model)
        {
            var semesterId = model.Semester.Id;
            var semester = _db.Semesters
                .Include(x => x.SemesterStudents).ThenInclude(x => x.Semester)
                .Include(x => x.SemesterStudents).ThenInclude(x => x.Student)
                .Include(x => x.Group).ThenInclude(x => x.Students).Where(x => x.Id == semesterId).FirstOrDefault();

            var students = semester.Group.Students.OrderBy(x => x.FIO).ToList();
            var counter = 0;
            foreach(var student in students)
            {
                var semesterStudent = semester.SemesterStudents.Where(x => x.Student.Id == student.Id).FirstOrDefault();
                if(semesterStudent is null)
                {
                    semester.SemesterStudents.Add(new EF.Custom.Entities.SemesterStudent() {Semester = semester, Student = student,SpecialyMark = model.SpecialyMark[counter]});
                }
                else
                {
                    semesterStudent.SpecialyMark = model.SpecialyMark[counter];
                }
                counter++;
            }
            _db.SaveChanges();

            return RedirectToAction("Index","Settings");
        }


    }
}
