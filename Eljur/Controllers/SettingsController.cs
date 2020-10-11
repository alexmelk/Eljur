using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eljur.Context.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eljur.Controllers
{
    public class SettingsController : Controller
    {
        Context.dbContext _db;
        public SettingsController(Context.dbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        //main view
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
        public IActionResult EditGroupView(int id)
        {
            var model = _db.Group.Include(a => a.Subjects).Where(x => x.Id == id).FirstOrDefault();
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
            var model = _db.Subject.Include(a => a.Themes).ToList();
            return View(model);
        }
        public IActionResult ThemeView()
        {
            var model = _db.Theme.Include(x=>x.Subject).ToList();
            return View(model);
        }
        public IActionResult EditThemeView(int id)
        {
            var model = _db.Theme.Include(x => x.Subject).Where(x => x.Id == id).FirstOrDefault();
            return View(model);
        }
        
        //visit
        public IActionResult AddVisit(int time)
        {
            return RedirectToAction("VisitView");
        }
        //group
        public IActionResult EditGroup(int? id, string name, int? subjectId)
        {
            var find = _db.Group.Include(x => x.Subjects).Where(x => x.Id == id).FirstOrDefault();

            if(find == null) //если нет, то добавляем
            {
                _db.Group.Add(new Group() { Name = name });
                _db.SaveChanges();
            }
            else
            {
                if (subjectId == null)
                {
                    find.Name = name;
                }
                else
                {
                    find.Subjects.Add(_db.Subject.Find(subjectId));
                    _db.SaveChanges();
                    var m = _db.Group.Include(a => a.Subjects).Where(x => x.Id == id).FirstOrDefault();
                    return View("EditGroupView", m);
                }
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
        public IActionResult AddGroup(string name)
        {
            return RedirectToAction("GroupView");
        }
        //student
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
        //subject
        public IActionResult EditSubject(int? id, string name)
        {
            var find = _db.Subject.Find(id);
            if (find == null)
            {
                _db.Subject.Add(new Subject() { Name = name });
                _db.SaveChanges();
            }
            else
            {
                find.Name = name;
                _db.SaveChanges();
            }

            var model = _db.Subject.Include(a => a.Themes).ToList();

            return RedirectToAction("SubjectView", model);
        }
        public IActionResult RemoveSubject(int id)
        {
            _db.Subject.Remove(_db.Subject.Find(id));
            _db.SaveChanges();

            var model = _db.Subject.Include(a => a.Themes).ToList();

            return RedirectToAction("SubjectView", model);
        }
        //themes
        public IActionResult EditTheme(int? id, string name, int subjectId)
        {
            var find = _db.Theme.Find(id);
            if (find == null)
            {
                _db.Theme.Add(new Theme() { Name = name, Subject = _db.Subject.Find(subjectId) });
                _db.SaveChanges();
            }

            var model = _db.Theme.Include(a => a.Subject).ToList();

            return RedirectToAction("ThemeView", model);
        }
        public IActionResult RemoveTheme(int id)
        {
            _db.Theme.Remove(_db.Theme.Find(id));
            _db.SaveChanges();

            var model = _db.Theme.Include(a => a.Subject).ToList();

            return RedirectToAction("ThemeView", model);
        }
        [HttpPost]
        public IActionResult EditTheme(Theme theme)
        {
            var savedTheme = _db.Theme.Find(theme.Id);

            savedTheme.Name = theme.Name;
            savedTheme.Subject = _db.Subject.Find(theme.Subject.Id);

            _db.SaveChanges();

            var model = _db.Theme.Include(a => a.Subject).ToList();

            return RedirectToAction("ThemeView", model);
        }
        //group-subject
        public IActionResult RemoveGroupSubject(int groupId, int subjectId)
        {
            var group = _db.Group.Include(x => x.Subjects).Where(x => x.Id == groupId).FirstOrDefault();
            var subject = group.Subjects.Where(x => x.Id == subjectId).FirstOrDefault();
            group.Subjects.Remove(subject);
            _db.SaveChanges();

            var model = _db.Group.Include(a => a.Subjects).Where(x => x.Id == groupId).FirstOrDefault();
            return View("EditGroupView", model);
        }
    }
}
