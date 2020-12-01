using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eljur.Context.Tables;
using Eljur.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Eljur.Models;

namespace Eljur.Controllers
{
    [Authorize(Roles = "admin, teacher")]
    public class SettingsController : Controller
    {
        public dbContext _db;

        public SettingsController(dbContext db)
        {
            _db = db;
        }
        /// <summary>
        /// Главная страница контроллера settings
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Посещаемость
        /// </summary>
        /// <returns></returns>
        //main view
        public IActionResult VisitView()
        {
            return View(new ChoosePropertyVisit());
        }
        /// <summary>
        /// Группы
        /// </summary>
        /// <returns></returns>
        public IActionResult GroupView()
        {
         var model = _db.Group.Include(a => a.Students)
                .Include(a => a.Subjects)
                .Include(a => a.GroupVisits)
                .ToList();
            return View(model);
        }
        /// <summary>
        /// Редактирование группы
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult EditGroupView(int id)
        {
            var model = _db.Group
                .Include(x => x.Subjects)
                .Include(x=>x.Students)
                .ThenInclude(x=>x.StudentVisits)
                .Where(x => x.Id == id).FirstOrDefault();

            return View(model);
        }
        /// <summary>
        /// Студенты
        /// </summary>
        /// <returns></returns>
        public IActionResult StudentView()
        {
            var model = _db.Student.Include(a => a.Group)
                .Include(a => a.StudentVisits)
                .ToList();

            return View(model);
        }
        /// <summary>
        /// Редактирование студентов
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult EditStudentView(int id)
        {
            var model = _db.Student.Include(x => x.Group).Where(x => x.Id == id).FirstOrDefault();
            return View(model);
        }
        /// <summary>
        /// Предметы
        /// </summary>
        /// <returns></returns>
        public IActionResult SubjectView()
        {
            var model = _db.Subject.Include(a => a.Themes).ToList();
            return View(model);
        }
        /// <summary>
        /// Темы занятий
        /// </summary>
        /// <returns></returns>
        public IActionResult ThemeView()
        {
            var model = _db.Theme
                .Include(x => x.Subject)
                .Include(x => x.ThemeGroup)
                .ToList();

            return View(model);
        }
        /// <summary>
        /// Редактирование тем занятий
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult EditThemeView(int id)
        {
            var model = _db.Theme
                .Include(x => x.Subject)
                .Include(x=>x.ThemeGroup)
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return View(model);
        }     
        /// <summary>
        /// Добавить посещение
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        //visit
        public IActionResult AddVisit(int time)
        {
            return RedirectToAction("VisitView", new ChoosePropertyVisit());
        }



        /// <summary>
        /// Редактировать или создать группу
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="subjectId"></param>
        /// <returns></returns>
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

                    var m = _db.Group
                        .Include(x => x.Subjects)
                        .Include(x=>x.Students)
                        .ThenInclude(x=>x.StudentVisits)
                        .Where(x => x.Id == id).FirstOrDefault();

                    return View("EditGroupView", m);
                }

                _db.SaveChanges();
            }

            var model = _db.Group.Include(a => a.Students)
                .Include(a => a.Subjects)
                .Include(a => a.GroupVisits)
                .ToList();

            return RedirectToAction("GroupView", model);
        }
        /// <summary>
        /// Удалить группу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult RemoveGroup(int id)
        {
            var group = _db.Group
                .Include(x => x.Students)
                .ThenInclude(x => x.StudentVisits)
                .Include(x => x.Subjects)
                .Include(x => x.GroupVisits)
                .Where(x => x.Id == id).FirstOrDefault();

            _db.Group.Remove(group);
            _db.SaveChanges();

             var model = _db.Group.Include(x => x.Students)
                .Include(x => x.Subjects)
                .Include(x => x.GroupVisits)
                .ToList();

            return RedirectToAction("GroupView", model);
        }
        /// <summary>
        /// Редактировать или создать студента
        /// </summary>
        /// <param name="id"></param>
        /// <param name="groupid"></param>
        /// <param name="fio"></param>
        /// <returns></returns>
        //student
        public IActionResult EditStudent(int? id, int? groupid, string fio)
        {
            var find = _db.Student.Find(id);

            if (find == null) //если нет, то добавляем
            {
                _db.Student.Add(new Student() { FIO = fio, Group = _db.Group.Find(groupid) });
                _db.SaveChanges();
            }

            var model = _db.Student.Include(x => x.Group)
                .Include(x => x.StudentVisits)
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

            var model = _db.Student.Include(x => x.Group)
                .Include(x => x.StudentVisits)
                .ToList();

            return RedirectToAction("StudentView", model);
        }
        /// <summary>
        /// Удаление студента
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult RemoveStudent(int id)
        {
            var student = _db.Student
                .Include(x => x.StudentVisits)
                .Include(x => x.Group)
                .Where(x => x.Id == id)
                .FirstOrDefault();

            _db.Student.Remove(student);
            _db.SaveChanges();

            var model = _db.Student.Include(x => x.Group)
                .Include(x => x.StudentVisits)
                .ToList();
            
            return RedirectToAction("StudentView", model);
        }
        /// <summary>
        /// Редактировать или создать предмет
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Удаление предмета
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult RemoveSubject(int id)
        {
            var subject = _db.Subject
                .Include(x => x.Themes)
                .ThenInclude(x => x.Visits)
                .Where(x => x.Id == id)
                .FirstOrDefault();

            subject.Themes.Clear();

            _db.Subject.Remove(subject);
            _db.SaveChanges();

            var model = _db.Subject.Include(a => a.Themes).ToList();

            return RedirectToAction("SubjectView", model);
        }
        /// <summary>
        /// Редактировать или создать тему
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        //themes
        public IActionResult EditTheme(int? id, string name, int subjectId, int hours, TypeSubjectEnum type)
        {
            var find = _db.Theme.Find(id);
            if (find == null)
            {
                _db.Theme.Add(new Theme() { Name = name, Subject = _db.Subject.Find(subjectId), Type = type, AllowedHours = hours, ThemeGroup = new ThemeGroup() });
                _db.SaveChanges();
            }

            var model = _db.Theme
                .Include(x => x.Subject)
                .Include(x => x.ThemeGroup)
                .ToList();

            return RedirectToAction("ThemeView", model);
        }
        [HttpPost]
        public IActionResult EditTheme(Theme theme)
        {
            var savedTheme = _db.Theme.Find(theme.Id);

            savedTheme.Name = theme.Name;
            savedTheme.Subject = _db.Subject.Find(theme.Subject.Id);
            savedTheme.Type = theme.Type;
            savedTheme.AllowedHours = theme.AllowedHours;

            _db.SaveChanges();

            var model = _db.Theme
                .Include(x => x.Subject)
                .Include(x => x.ThemeGroup)
                .ToList();

            return RedirectToAction("ThemeView", model);
        }
        /// <summary>
        /// Удалить тему
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult RemoveTheme(int id)
        {
            var theme = _db.Theme
                .Include(x => x.Subject)
                .Include(x=>x.Visits)
                .Where(x => x.Id == id)
                .FirstOrDefault();

            theme.Visits.Clear();

            _db.Theme.Remove(theme);
            _db.SaveChanges();

            var model = _db.Theme
                .Include(x => x.Subject)
                .Include(x => x.ThemeGroup)
                .ToList();

            return RedirectToAction("ThemeView", model);
        }
        /// <summary>
        /// Удалить прикрепление предмета к группе
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="subjectId"></param>
        /// <returns></returns>
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

        public IActionResult ClearVisits(int id)
        {
            var group = _db.Group.Include(x => x.GroupVisits).ThenInclude(x => x.StudentVisits).FirstOrDefault();

            group.GroupVisits.Clear();

            _db.SaveChanges();

            return RedirectToAction("GroupView");
        }
    }
}
