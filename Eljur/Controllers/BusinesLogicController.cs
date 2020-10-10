using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eljur.Context.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eljur.Controllers
{
    public class BusinesLogicController : Controller
    {
        Context.dbContext _db;
        public BusinesLogicController(Context.dbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult VisitView()
        {
            return View();
        }
        public IActionResult GroupView()
        {
         var model = _db.Group.Include(a => a.Students)
                .Include(a => a.Subjects)
                .Include(a => a.Visits)
                .ToList();
            return View(model);
        }
        public IActionResult StudentView()
        {
            var model = _db.Student.Include(a => a.Group)
                .Include(a => a.Visits)
                .ToList();
            return View(model);
        }
        public IActionResult EditStudentView(int id)
        {
            var model = _db.Student.Include(x => x.Group).Where(x => x.Id == id).FirstOrDefault();
            return View(model);
        }
        public IActionResult SubjectView()
        {
            return View();
        }
        public IActionResult ThemeView()
        {
            return View();
        }



        public IActionResult AddVisit(int time)
        {
            return RedirectToAction("VisitView");
        }

        public IActionResult EditGroup(int? id, string name)
        {
            var find = _db.Group.Find(id);

            if(find == null) //если нет, то добавляем
            {
                _db.Group.Add(new Group() { Name = name });
                _db.SaveChanges();
            }
            else
            {
                find.Name = name;
                _db.SaveChanges();
            }
            var model = _db.Group.Include(a => a.Students)
                .Include(a => a.Subjects)
                .Include(a => a.Visits)
                .ToList();
            return RedirectToAction("GroupView", model);
        }
        public IActionResult RemoveGroup(int id)
        {
            _db.Group.Remove(_db.Group.Find(id));
            _db.SaveChanges();

             var model = _db.Group.Include(a => a.Students)
                .Include(a => a.Subjects)
                .Include(a => a.Visits)
                .ToList();
            return RedirectToAction("GroupView", model);
        }

        public IActionResult EditStudent(int? id, int? groupid, string fio)
        {
            var find = _db.Student.Find(id);

            if (find == null) //если нет, то добавляем
            {
                _db.Student.Add(new Student() { FIO = fio, Group = _db.Group.Find(groupid) });
                _db.SaveChanges();
            }
            var model = _db.Student.Include(a => a.Group)
                .Include(a => a.Visits)
                .ToList();
            return RedirectToAction("StudentView", model);
        }
        [HttpPost]
        public IActionResult EditStudent(Student student)
        {
            var savedStudent = _db.Student.Find(student.Id);

            savedStudent.FIO = student.FIO;
            savedStudent.Group = _db.Group.Find(student.Group.Id);

            _db.SaveChanges();

            var model = _db.Student.Include(a => a.Group)
                .Include(a => a.Visits)
                .ToList();
            return RedirectToAction("StudentView", model);
        }
        public IActionResult RemoveStudent(int id)
        {
            _db.Student.Remove(_db.Student.Find(id));

            _db.SaveChanges();
            var model = _db.Student.Include(a => a.Group)
                .Include(a => a.Visits)
                .ToList();
            return RedirectToAction("StudentView", model);
        }


        public IActionResult AddGroup(string name)
        {
            return RedirectToAction("GroupView");
        }
    }
}
