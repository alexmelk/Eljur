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
            var model = _db.Group
                   .Include(x => x.Specialization)
                   .Include(x => x.Students)
                   .Include(x => x.Semesters)
                   .ThenInclude(x => x.GroupVisits)
                   .Include(x => x.Semesters)
                   .ThenInclude(x => x.Subjects)
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
                .Include(x => x.Specialization)
                .Include(x => x.Semesters)
                .ThenInclude(x => x.GroupVisits)
                .Include(x => x.Semesters)
                .ThenInclude(x => x.Subjects)
                .Include(x => x.Students)
                .ThenInclude(x => x.StudentVisits)
                .Where(x => x.Id == id)
                .FirstOrDefault();

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
            var model = _db.Subject.Include(a => a.Themes).Include(x => x.Teacher).ToList();
            return View(model);
        }
        /// <summary>
        /// Темы занятий
        /// </summary>
        /// <returns></returns>
        public IActionResult ThemeView()
        {

            var model = _db.Theme.Include(x => x.Subject).ThenInclude(x => x.Semester)
                   .Include(x => x.Subject).ThenInclude(x => x.Group).ThenInclude(x => x.Semesters)
                   .Include(x => x.Subject).ThenInclude(x => x.Group).ThenInclude(x => x.Specialization)
                   .Include(x => x.ThemeGroup)
                   .ToList();

            return View(model);
        }
        /// <summary>
        /// Уровень образования(бакалавриат, специалитет...)
        /// </summary>
        /// <returns></returns>
        public IActionResult EducationLevelsView()
        {
            var model = _db.EducationLevels
                .Include(x => x.EducationDepartments)
                .ToList();

            return View(model);
        }
        [HttpPost]
        public IActionResult EditEducationLevels(int? id, string name)
        {
            if (id is null)
            {
                var model = new EducationLevel() { Name = name };
                _db.EducationLevels.Add(model);
                _db.SaveChanges();

            }
            else
            {
                var model = _db.EducationLevels.Find(id);
                model.Name = name;
                _db.SaveChanges();

            }
            return RedirectToAction("EducationLevelsView");
        }
        public IActionResult RemoveEducationLevels(int id)
        {
            var model = _db.EducationLevels
                .Include(x => x.EducationDepartments)
                .Where(x => x.Id == id)
                .FirstOrDefault();

            _db.EducationLevels.Remove(model);
            _db.SaveChanges();
            return RedirectToAction("EducationLevelsView");
        }
        /// <summary>
        /// Отделения(очное, заочное)
        /// </summary>
        /// <returns></returns>
        public IActionResult EducationDepartmentsView()
        {
            var model = _db.EducationDepartments
                   .Include(x => x.Specializations)
                   .ToList();

            return View(model);
        }
        public IActionResult RemoveEducationDepartment(int id)
        {
            var model = _db.EducationDepartments.Find(id);
            _db.EducationDepartments.Remove(model);
            _db.SaveChanges();
            return RedirectToAction("EducationDepartmentsView");
        }
        public IActionResult EditEducationDepartment(int id, int educationLevelId, string name)
        {
            if (id == 0)
            {

                var educLevels = _db.EducationLevels
                    .Include(x => x.EducationDepartments)
                    .Where(x => x.Id == educationLevelId)
                    .FirstOrDefault();

                educLevels.EducationDepartments.Add(new EducationDepartment() { Name = name });
                _db.SaveChanges();
            }
            else
            {
                var educDep = _db.EducationDepartments.Find(id);
                educDep.Name = name;
                _db.SaveChanges();
            }
            return RedirectToAction("EducationDepartmentsView");
        }
        /// <summary>
        /// Специализации
        /// </summary>
        /// <returns></returns>
        public IActionResult SpecializationsView()
        {
            var model = _db.Specializations
                .Include(x => x.Groups)
                .ToList();

            return View(model);
        }
        public IActionResult RemoveSpecialization(int id)
        {
            var model = _db.Specializations.Find(id);
            _db.Specializations.Remove(model);
            _db.SaveChanges();
            return RedirectToAction("SpecializationsView");
        }
        public IActionResult EditSpecialization(int id, string departAndLevel, string name)
        {
            if (id == 0)
            {
                var arr = departAndLevel.Split(",");
                var departId = Convert.ToInt32(arr[0]);
                var levelId = Convert.ToInt32(arr[1]);

                var educDepart = _db.EducationDepartments
                    .Include(x => x.Specializations)
                    .Include(x=>x.EducationLevels)
                    .Where(x => x.Id == departId)
                    .FirstOrDefault();

                educDepart.Specializations.Add(new Specialization() { Name = name, EducationLevel = _db.EducationLevels.Find(levelId) });
                _db.SaveChanges();
            }
            else
            {
                var spec = _db.Specializations.Find(id);
                spec.Name = name;
                _db.SaveChanges();
            }
            return RedirectToAction("SpecializationsView");
        }
        public IActionResult EditGroupSpecialization(int id, int specializationId)
        {
            var spec = _db.Specializations
                .Include(x => x.Groups)
                .Where(x => x.Id == specializationId)
                .FirstOrDefault();

            var m = _db.Group
                       .Include(x => x.Specialization)
                       .Include(x => x.Semesters)
                       .ThenInclude(x => x.GroupVisits)
                       .Include(x => x.Semesters)
                       .ThenInclude(x=>x.Subjects)
                       .Include(x => x.Students)
                       .ThenInclude(x => x.StudentVisits)
                       .Where(x => x.Id == id).FirstOrDefault();
            m.Specialization = spec;
            _db.SaveChanges();

            return View("EditGroupView", m);
        }
        /// <summary>
        /// Семестры
        /// </summary>
        /// <returns></returns>
        public IActionResult SemestersView()
        {
            var model = _db.Semesters
                .Include(x => x.Group)
                .ThenInclude(x=>x.Specialization)
                .ThenInclude(x => x.EducationDepartment)
                .ThenInclude(x => x.EducationLevels)
                .Include(x => x.Subjects)
                .ToList();

            return View(model);
        }
        public IActionResult EditSemester(int groupId, int semesterNumber)
        {
            var group = _db.Group.Include(x => x.Semesters).Where(x => x.Id == groupId).FirstOrDefault();

            if (group.Semesters.TrueForAll(x => x.Number != semesterNumber))
            {
                group.Semesters.Add(new Semester { Group = group, Number = semesterNumber });
                _db.SaveChanges();
            }
            return RedirectToAction("SemestersView");
        }

        /// <summary>
        /// Преподаватели
        /// </summary>
        /// <returns></returns>
        public IActionResult TeachersView()
        {
            var model = _db.Teachers
                .Include(x => x.Subjects)
                .ToList();

            return View(model);
        }
        public IActionResult RemoveTeacher(int id)
        {
            var model = _db.Teachers.Find(id);
            _db.Teachers.Remove(model);
            _db.SaveChanges();
            return RedirectToAction("TeachersView");
        }
        [HttpPost]
        public IActionResult EditTeacher(int? id, string name)
        {
            if (id is null)
            {
                var model = new Teacher() { FIO = name };
                _db.Teachers.Add(model);
                _db.SaveChanges();

            }
            else
            {
                var model = _db.Teachers.Find(id);
                model.FIO = name;
                _db.SaveChanges();

            }
            return RedirectToAction("TeachersView");
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
                .Include(x => x.ThemeGroup)
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
            var find = _db.Group
                .Include(x => x.Semesters)
                .ThenInclude(x => x.GroupVisits)
                .Include(x => x.Semesters)
                .ThenInclude(x => x.Subjects)
                .Where(x => x.Id == id).FirstOrDefault();

            if (find == null) //если нет, то добавляем
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
                    //доделать
/*                    find.Subjects.Add(_db.Subject.Find(subjectId));         */

                    _db.SaveChanges();

                 var m = _db.Group
                .Include(x => x.Semesters)
                .ThenInclude(x => x.GroupVisits)
                .Include(x => x.Semesters)
                .ThenInclude(x => x.Subjects)
                .Where(x => x.Id == id)
                .FirstOrDefault();

                    return View("EditGroupView", m);
                }

                _db.SaveChanges();
            }

            var model = _db.Group
                .Include(a=>a.Specialization)
                .Include(a => a.Students)
                .Include(x=>x.Semesters)
                .ThenInclude(x=>x.GroupVisits)
                .Include(x=>x.Semesters)
                .ThenInclude(x=>x.Subjects)
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
                .Include(x => x.Semesters)
                .ThenInclude(x => x.GroupVisits)
                .Include(x => x.Semesters)
                .ThenInclude(x => x.Subjects)
                .Where(x => x.Id == id)
                .FirstOrDefault();

            _db.Group.Remove(group);
            _db.SaveChanges();

            var model = _db.Group
               .Include(x=>x.Specialization)
               .Include(x => x.Students)
               .Include(x=>x.Semesters)
               .ThenInclude(x=>x.GroupVisits)
               .Include(x=>x.Semesters)
               .ThenInclude(x=>x.Subjects)
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
        public IActionResult EditSubject(int? id, string name, int teacherId, int groupId, int semesterNumber, AttestationEnum attestationEnum, string attestationHouse)
        {
            var semesters = _db.Group
                .Include(x => x.Semesters)
                .ThenInclude(x => x.Subjects)
                .ThenInclude(x => x.Teacher)
                .Where(x => x.Id == groupId)
                .FirstOrDefault()
                .Semesters;

            var find = _db.Subject.Find(id);
            if (find == null)               //предмет ранее не добавлен
            {
                var selectedSemester = semesters.Where(x => x.Number == semesterNumber).FirstOrDefault();
                var semesterExist = selectedSemester is null ? false : true;

                if (semesterExist)
                {
                    selectedSemester.Subjects.Add(new Subject { Name = name, 
                        Teacher = _db.Teachers.Find(teacherId), 
                        Group = _db.Group.Find(groupId), 
                        Attestation = attestationEnum, 
                        AttestationHours = double.Parse(attestationHouse.Replace(".", ","))
                    });
                    _db.SaveChanges();
                }
            }
            else                            //редактируем
            {
                if (!string.IsNullOrEmpty(name))
                {
                    find.Name = name;
                }
                find.Teacher = _db.Teachers.Find(teacherId);

                _db.SaveChanges();
            }

            var model = _db.Subject.Include(a => a.Themes).Include(x => x.Teacher).ToList();

            return RedirectToAction("SubjectView", model);
        }
        /// <summary>
        /// Удаление предмета
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// TODO: refactor DB
        public IActionResult RemoveSubject(int id)
        {
            var subject = _db.Subject
                .Include(x=>x.Semester.Group)
                .Include(x => x.Teacher)
                .Include(x => x.Themes)
                .Include(x => x.Themes).ThenInclude(x => x.ThemeGroup)
                .Include(x=>x.Semester).ThenInclude(x=>x.GroupVisits).ThenInclude(x=>x.ThemeVisits)
                .Include(x => x.Semester).ThenInclude(x => x.GroupVisits).ThenInclude(x => x.StudentVisits)
                .Where(x => x.Id == id)
                .FirstOrDefault();

            var themeVisit = _db.ThemeVisit.Include(x => x.Theme).ToList();
            var needRemove = themeVisit.Where(x => subject.Themes.Any(y => y.Id == x.Theme.Id)).ToList();
            themeVisit.RemoveAll(x=>needRemove.Contains(x));

            var groupVisit = _db.GroupVisit.Include(x => x.Subject).ToList();
            var needRemoveGroupVisit = groupVisit.Where(x => subject.Id==x.Subject.Id).ToList();
            groupVisit.RemoveAll(x => needRemoveGroupVisit.Contains(x));

            var studentVisit = _db.StudentVisit.Include(x => x.Subject).ToList();
            var needRemoveStudentVisit = studentVisit.Where(x => subject.Id == x.Subject.Id).ToList();
            studentVisit.RemoveAll(x => needRemoveStudentVisit.Contains(x));

            _db.SaveChanges();
            _db.Subject.Remove(subject);
            _db.SaveChanges();

            var model = _db.Subject.Include(a => a.Themes).Include(x=>x.Teacher).ToList();

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
        public IActionResult EditTheme(int? id, string name, int subjectId, double hours, TypeSubjectEnum type)
        {
            var find = _db.Theme.Find(id);
            if (find == null)
            {
                _db.Theme.Add(new Theme() { Name = name, Subject = _db.Subject.Find(subjectId), Type = type, AllowedHours = hours, ThemeGroup = new ThemeGroup(),
                    Semester = _db.Subject.Include(x=>x.Semester).Where(x=>x.Id==subjectId).FirstOrDefault().Semester});
                _db.SaveChanges();
            }

            return RedirectToAction("ThemeView");
        }
        [HttpPost]
        public IActionResult EditTheme(Theme theme, string allowedHours)
        {
            var savedTheme = _db.Theme.Find(theme.Id);

            savedTheme.Name = theme.Name;
            savedTheme.Subject = _db.Subject.Find(theme.Subject.Id);
            savedTheme.Type = theme.Type;
            savedTheme.AllowedHours = double.Parse(allowedHours.Replace(".", ","));

            _db.SaveChanges();

            return RedirectToAction("ThemeView");
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
                .Where(x => x.Id == id)
                .FirstOrDefault();

            _db.Theme.Remove(theme);
            _db.SaveChanges();

            return RedirectToAction("ThemeView");
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
            var group = _db.Group
                .Include(x => x.Semesters)
                .ThenInclude(x => x.GroupVisits)
                .Include(x => x.Semesters)
                .ThenInclude(x => x.Subjects)
                .Where(x => x.Id == groupId).FirstOrDefault();

            //доделать
            /*            var subject = group.Subjects.Where(x => x.Id == subjectId).FirstOrDefault();
                        group.Subjects.Remove(subject);*/
            _db.SaveChanges();

            var model = _db.Group
                .Include(x => x.Semesters)
                .ThenInclude(x => x.GroupVisits)
                .Include(x => x.Semesters)
                .ThenInclude(x => x.Subjects)
                .Where(x => x.Id == groupId)
                .FirstOrDefault();
            return View("EditGroupView", model);
        }

        public IActionResult ClearVisits(int id)
        {
            var group = _db.Group
                .Include(x => x.Specialization)
                .Include(x => x.Semesters)
                .ThenInclude(x => x.GroupVisits)
                .Include(x => x.Semesters)
                .ThenInclude(x => x.Subjects)
                .Include(x => x.Students)
                .ThenInclude(x => x.StudentVisits)
                .FirstOrDefault();

            group.Semesters.Clear();

            _db.SaveChanges();

            return RedirectToAction("GroupView");
        }

        public IActionResult UsersView()
        {
            var users = _db.Users;
            var model = new AdminViewModel() { Users = users.ToList() };

            return View(model);
        }
    }
}
