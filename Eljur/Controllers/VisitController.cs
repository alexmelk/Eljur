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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace Eljur.Controllers
{
    [Authorize(Roles = "admin, teacher, dekan, dekanat")]
    public partial class VisitController : Controller
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
        public IActionResult ChooseSubject(ChoosePropertyVisit input)
        {
            input.Group = _db.Group.Include(x => x.Students).Where(x => x.Id == input.Group.Id).FirstOrDefault();
            input.Subject = _db.Subject.Include(x => x.Themes).Where(x => x.Id == input.Subject.Id).FirstOrDefault();

            var visits = new List<VisitModify>();
            for (int j = 0; j < input.Group.Students.Count(); j++) visits.Add(new VisitModify());

            var output = new VisitGroupForColumnModel() { VisitsModify = visits };

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

            var saved = HttpContext.Session.GetString("SessionStorageThemes");
            var storage = new List<SessionStorageThemes>();
            if (storage != null)
            {
                storage = JsonConvert.DeserializeObject<List<SessionStorageThemes>>(saved);
            }

            var themeVisits = new List<ThemeVisit>();
            foreach (var savedTheme in storage)
            {
                var theme = _db.Theme.Include(x => x.ThemeGroup).Include(x=>x.Subject).ThenInclude(x=>x.Semester).Where(x => x.Id == savedTheme.ThemeId).FirstOrDefault();

                theme.ThemeGroup.UsedHours = savedTheme.ThemeGroup.UsedHours;
                var hoursPerVisit = savedTheme.Reserved;
                var typeSubject = theme.Type;
                var themeVisit = new ThemeVisit() { HoursPerVisit = hoursPerVisit, Theme = theme, TypeSubject = typeSubject };

                themeVisits.Add(themeVisit);
            }

            var visit = new GroupVisit()
            {
                Date = date,
                ThemeVisits = themeVisits,
                Subject = _db.Subject.Find(model.SubjectId),
                Group = _db.Group.Find(model.GroupId),
                Semester = _db.Subject.Include(x=>x.Semester).Where(x=>x.Id==model.SubjectId).FirstOrDefault().Semester,
                Theme = themeVisits.FirstOrDefault().Theme
            };
            _db.GroupVisit.Add(visit);

            foreach (var visitModify in model.Output.VisitsModify)
            {
                var studentVisit = new StudentVisit()
                {
                    TypeVisit = visitModify.TypeVisit,
                    Student = _db.Student.Find(visitModify.StudentId),
                    GroupVisit = visit,
                    Subject = _db.Subject.Find(model.SubjectId),
                    Theme = visit.ThemeVisits.FirstOrDefault().Theme
                };
                _db.StudentVisit.Add(studentVisit);
                visit.StudentVisits.Add(studentVisit);
                visit.Theme.StudentVisits.Add(studentVisit);
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

            var visits = _db.GroupVisit
                .Include(x => x.StudentVisits)
                .ThenInclude(x => x.Student)
                .Include(x => x.Subject)
                .Include(x => x.Group)
                .Include(x => x.ThemeVisits)
                .ThenInclude(x => x.Theme)
                .Where(x => x.Group.Id==model.Group.Id).ToList();

            var output = new GroupVisitView()
            {
                Semester = model.Semester,
                GroupId = model.Group.Id,
                Visits = visits,
                Filter = new Filter() { From = visits.Count == 0 ? DateTime.Now : visits.Min(x => x.Date), Before = visits.Count == 0 ? DateTime.Now : visits.Max(x => x.Date) }
            };

            return View("VisitTableView", output);
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
                .Include(x => x.ThemeVisits)
                .Where(x => ((x.Subject.Id == subjectId)
                        && (x.StudentVisits.FirstOrDefault().Student.Group.Id == groupId)))
                .ToList();

            var output = new GroupVisitView()
            {
                GroupId = groupId,
                SubjectId = subjectId,
                Visits = visits,
                Filter = new Filter() { From = visits.Min(x => x.Date), Before = visits.Max(x => x.Date) }
            };

            return View("VisitTableView", output);
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
                             .Include(x => x.ThemeVisits)
                             .Where(x => ((x.Subject.Id == subjectId)
                             && (x.StudentVisits.FirstOrDefault().Student.Group.Id == groupId)))
                             .ToList();

            var output = new GroupVisitView()
            {
                GroupId = groupId,
                SubjectId = subjectId,
                Visits = visits,
                Filter = new Filter() { From = visits.Min(x => x.Date), Before = visits.Max(x => x.Date) }
            };

            return View("VisitTableView", output);

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

            var group = _db.Group
                .Include(x => x.Semesters)
                .ThenInclude(x => x.GroupVisits)
                .Include(x => x.Semesters)
                .ThenInclude(x => x.Subjects)
                .Where(x => x.Id == model.Group.Id)
                .FirstOrDefault();

            var subject = group.Semesters.Where(x => x.Number == model.Semester).FirstOrDefault()?.Subjects.ToList();

            if ((subject is null) || (subject.Count() == 0))
            {
                return View("NoVisits", model);
            }
            else
            if ((subject is null) && (action != "create-for-group"))
            {
                return View("NoVisits", model);
            }

            switch (action)
            {
/*                case "create": { return GetExcel(model, allSubjects: false); }*/
                case "edit": { return EditGroupVisit(model); }
                case "create-for-group": { return GetExcel(model, allSubjects: true); }
            }
            return View("VisitView");
        }
        /// <summary>
        /// Создаём excel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IActionResult GetExcel(ChoosePropertyVisit model, bool allSubjects)
        {
            return GenerateExcel(model, allSubjects);
        }

        public FileResult GenerateExcel(ChoosePropertyVisit model, bool allSubjects)
        {
            bool firstSubject = true;
            var page = 8;
            var subjectsModel = new List<SubjectsModel>();

            Stream stream = new FileStream(path: ".//file.xlsx", FileMode.Create);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var outputPakage = new ExcelPackage())
            {

                var group = _db.Group
                    .Include(x => x.Semesters).ThenInclude(x => x.SemesterStudents).ThenInclude(x => x.Student)
                    .Include(x => x.Students).ThenInclude(x => x.StudentVisits).ThenInclude(x => x.GroupVisit).ThenInclude(x => x.Subject)
                    .Include(x => x.Students).ThenInclude(x => x.StudentVisits).ThenInclude(x => x.GroupVisit).ThenInclude(x => x.Group)
                    .Include(x => x.Semesters).ThenInclude(x => x.GroupVisits).ThenInclude(x => x.ThemeVisits).ThenInclude(x => x.Theme)
                    .Include(x => x.Semesters).ThenInclude(x => x.Subjects).ThenInclude(x=>x.Teacher)
                    .Include(x => x.Semesters).ThenInclude(x => x.Comments)
                    .Include(x => x.Semesters).ThenInclude(x => x.Checks)
                    .Include(x => x.Specialization).ThenInclude(x => x.EducationDepartment)
                    
                    .Where(x => x.Id == model.Group.Id).FirstOrDefault();

                var students = group.Students.OrderBy(x => x.FIO).ToList();

                var subjectsList = new List<Subject>();
                subjectsList.AddRange(group.Semesters.Where(x=>x.Number==model.Semester).FirstOrDefault().Subjects);
                var allowAddNextSemester = group.Semesters.Any(x => x.Number == model.Semester + 1);

                if (allowAddNextSemester)
                    subjectsList.AddRange(group.Semesters.Where(x => x.Number == model.Semester + 1).FirstOrDefault().Subjects);

                var subjects = allSubjects ? subjectsList : new List<Subject>() { model.Subject };
                //посещения
                foreach (var subject in subjects.OrderBy(x => x.Name).ThenBy(x => x.Semester.Number).ToList())
                {
                    using (var package = new ExcelPackage(new FileInfo(".//template.xlsx")))
                    {
                        GenerateVisits(students, model, package, group, subject, outputPakage, ref page, subjectsModel);
                    }
                }

                //начало
                using (var package = new ExcelPackage(new FileInfo(".//template.xlsx")))
                {

                        var minDate = subjectsList.Select(x => x.Semester?.GroupVisits.Min(x => x.Date)).FirstOrDefault();

                        var firstVisit = minDate is null ? DateTime.Now.Year : minDate.Value.Year;
                        //год начала
                        var firstYear = package.Workbook.Names[$"_1_1_firstYear"];
                        package.Workbook.Worksheets["Начало"].Cells[firstYear.Address].Value = firstVisit;

                        //год окончания
                        var secondYear = package.Workbook.Names[$"_1_1_secondYear"];
                        package.Workbook.Worksheets["Начало"].Cells[secondYear.Address].Value = firstVisit + 1;

                        //отделение
                        var educationDepartment = package.Workbook.Names[$"_1_1_EducationDepartment"];
                        package.Workbook.Worksheets["Начало"].Cells[educationDepartment.Address].Value = group.Specialization.EducationDepartment.Name;

                        //название группы
                        var groupName = package.Workbook.Names[$"_1_1_GroupName"];
                        package.Workbook.Worksheets["Начало"].Cells[groupName.Address].Value = group.Name;

                        //специализация
                        var specialization = package.Workbook.Names[$"_1_1_Specializaton"];
                        package.Workbook.Worksheets["Начало"].Cells[specialization.Address].Value = group.Specialization.Name;

                        //страница 2
                        var firstSemester = group.Semesters.Where(x => x.Number == model.Semester).FirstOrDefault().SemesterStudents.OrderBy(x => x.Student.FIO).ToList();

                        var counter = 1;
                        foreach (var semesterStudent in firstSemester)
                        {
                            var studentFio = package.Workbook.Names[$"_1_2_Student{counter}"];
                            package.Workbook.Worksheets["Начало"].Cells[studentFio.Address].Value = semesterStudent.Student.FIO;

                            if (allowAddNextSemester)
                            {
                                var secondSemester = group.Semesters.Where(x => x.Number == model.Semester + 1).FirstOrDefault().SemesterStudents.Where(x => x.Student.Id == semesterStudent.Student.Id).FirstOrDefault(); ;
                                var studentsMark = package.Workbook.Names[$"_1_2_OO{counter}"];

                                package.Workbook.Worksheets["Начало"].Cells[studentsMark.Address].Value = semesterStudent.SpecialyMark + " " + secondSemester.SpecialyMark;

                            }
                            else
                            {
                                var studentsMark = package.Workbook.Names[$"_1_2_OO{counter}"];
                                package.Workbook.Worksheets["Начало"].Cells[studentsMark.Address].Value = semesterStudent.SpecialyMark;
                            }

                            counter++;
                        }

                        //страница 3
                        var subjectsForList3 = subjectsModel.GroupBy(x => x.Subject.Name);
                        counter = 1;
                        foreach (var subj in subjectsForList3)
                        {
                            var subjectName = package.Workbook.Names[$"_1_3_Subject{counter}"];
                            package.Workbook.Worksheets["Начало"].Cells[subjectName.Address].Value = subj.First().Subject.Name;

                            var trud = package.Workbook.Names[$"_1_3_Trud{counter}"];

                            var time = 0.0;
                            if (subj.Count() == 1)
                            {
                                time = subj.FirstOrDefault().Subject.Themes.Sum(x => x.AllowedHours);
                            }
                            else
                            {
                                time = subj.FirstOrDefault().Subject.Themes.Sum(x => x.AllowedHours);
                                time += subj.LastOrDefault().Subject.Themes.Sum(x => x.AllowedHours);
                            }
                            package.Workbook.Worksheets["Начало"].Cells[trud.Address].Value = time;


                            var teacherName = package.Workbook.Names[$"_1_3_FIO{counter}"];
                            package.Workbook.Worksheets["Начало"].Cells[teacherName.Address].Value = subj.First().Subject.Teacher.FIO;

                            var str = package.Workbook.Names[$"_1_3_Str{counter}"];
                            package.Workbook.Worksheets["Начало"].Cells[str.Address].Value = subj.First().Page + " - " + (subj.Last().Page + 1);


                            counter++;
                        }


                        outputPakage.Workbook.Worksheets.Add("Начало", package.Workbook.Worksheets["Начало"]);
                        outputPakage.Workbook.Worksheets.MoveToStart(outputPakage.Workbook.Worksheets["Начало"].Index);
                        firstSubject = false;
                    
                }

                //конец
                using (var package = new ExcelPackage(new FileInfo(".//template.xlsx")))
                {
                    //Замечания преподавателей
                    var commentsForFirstSem = group.Semesters.Where(x => x.Number == model.Semester).FirstOrDefault().Comments;
                    var counter = 1;
                    foreach(var com in commentsForFirstSem)
                    {
                        var teacherDescr = package.Workbook.Names[$"_3_1_TeacherDescr{counter}"];
                        package.Workbook.Worksheets["Конец"].Cells[teacherDescr.Address].Value = com.TeacherDescription;

                        var dekanDescr = package.Workbook.Names[$"_3_1_DekanDescr{counter}"];
                        package.Workbook.Worksheets["Конец"].Cells[dekanDescr.Address].Value = com.DekanDescription;

                        counter++;
                    }

                    counter = 1;

                    if (allowAddNextSemester)
                    {
                        var commentsForSecondSem = group.Semesters.Where(x => x.Number == model.Semester+1).FirstOrDefault().Comments;
                        foreach (var com in commentsForSecondSem)
                        {
                            var teacherDescr = package.Workbook.Names[$"_3_2_TeacherDescr{counter}"];
                            package.Workbook.Worksheets["Конец"].Cells[teacherDescr.Address].Value = com.TeacherDescription;

                            var dekanDescr = package.Workbook.Names[$"_3_2_DekanDescr{counter}"];
                            package.Workbook.Worksheets["Конец"].Cells[dekanDescr.Address].Value = com.DekanDescription;

                            counter++;
                        }
                    }

                    //контроль деканата за ведением журнала

                    var checksForFirstSem = group.Semesters.Where(x => x.Number == model.Semester).FirstOrDefault().Checks;
                    counter = 1;
                    foreach (var check in checksForFirstSem)
                    {
                        if (check.Date != default)
                        {
                            var date = package.Workbook.Names[$"_3_3_date{counter}"];
                            package.Workbook.Worksheets["Конец"].Cells[date.Address].Value = check.Date.ToString("dd.MM.yyyy");
                        }

                        var result = package.Workbook.Names[$"_3_3_result{counter}"];
                        package.Workbook.Worksheets["Конец"].Cells[result.Address].Value = check.Text;

                        counter++;
                    }

                    counter = 1;

                    if (allowAddNextSemester)
                    {
                        var checksForSecondSem = group.Semesters.Where(x => x.Number == model.Semester + 1).FirstOrDefault().Checks;
                        foreach (var check in checksForSecondSem)
                        {
                            if (check.Date != default)
                            {
                                var date = package.Workbook.Names[$"_3_4_date{counter}"];
                                package.Workbook.Worksheets["Конец"].Cells[date.Address].Value = check.Date.ToString("dd.MM.yyyy");
                            }
                            var result = package.Workbook.Names[$"_3_4_result{counter}"];
                            package.Workbook.Worksheets["Конец"].Cells[result.Address].Value = check.Text;

                            counter++;
                        }
                    }

                    outputPakage.Workbook.Worksheets.Add("Конец", package.Workbook.Worksheets["Конец"]);
                }
                outputPakage.SaveAs(stream);

                stream.Position = 0;
                return File(stream, "application/xlsx", $"Журнал[{model.Group.Name}].xlsx");
            }
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
                            list.Add("НБ");
                            inValidHours += 2;
                            break;
                        }
                    case TypeVisitEnum.ValidAbsent:
                        {
                            list.Add("Б");
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
            package.Workbook.Worksheets["Посещаемость"].Cells[student.Address].Value = visitsMas;
            var valid = package.Workbook.Names[$"valid{index + 1}"];
            package.Workbook.Worksheets["Посещаемость"].Cells[valid.Address].Value = validHours;
            var inValid = package.Workbook.Names[$"invalid{index + 1}"];
            package.Workbook.Worksheets["Посещаемость"].Cells[inValid.Address].Value = inValidHours;

        }
        public void GenerateVisits(List<Student> students, ChoosePropertyVisit model, ExcelPackage package, Group group, Subject subject, ExcelPackage outputPakage, ref int page, List<SubjectsModel> subjectsModel)
        {
            //посещаемость студентов
            for (int i = 0; i < students.Count(); i++)
            {
                var visits = students[i].StudentVisits
                    .Where(x => x.GroupVisit.Subject.Id == subject.Id)
                    .Where(x => x.GroupVisit.Group.Id == model.Group.Id)
                    .ToList();

                WriteStudentsVisits(visits, package, i);
            }

            //название предмета
            var subjectName = package.Workbook.Names[$"SubjectName"];
            package.Workbook.Worksheets["Посещаемость"].Cells[subjectName.Address].Value = subject.Name;
            subjectsModel.Add(new SubjectsModel() { Subject = subject, Page = page });

            //семестр
            var semesterNumberAdress = package.Workbook.Names["semesterNumber"];
            package.Workbook.Worksheets["Посещаемость"].Cells[semesterNumberAdress.Address].Value = subject.Semester.Number;

            //учёт занятий
            var groupVisits = group.Semesters.Where(x => x.Number == subject.Semester.Number).FirstOrDefault().GroupVisits.Where(x => x.Subject.Id == subject.Id).ToList();
            var themeVisits = 1;
            for (int i = 0; i < groupVisits.Count(); i++)
            {
                var data = groupVisits[i];
                var date = data.Date.ToString("dd.MM.yyyy");
                var datePlace = package.Workbook.Names[$"time{i + 1}"]; //left page
                package.Workbook.Worksheets["Посещаемость"].Cells[datePlace.Address].Value = date;


                foreach (var theme in data.ThemeVisits)
                {
                    var datePlace1 = package.Workbook.Names[$"date_{themeVisits}"]; //right page
                    package.Workbook.Worksheets["Посещаемость"].Cells[datePlace1.Address].Value = date;

                    var visitType = package.Workbook.Names[$"visitType{themeVisits}"];
                    package.Workbook.Worksheets["Посещаемость"].Cells[visitType.Address].Value = (TypeSubjectRusEnum)theme.TypeSubject;

                    var themeName = package.Workbook.Names[$"themeName{themeVisits}"];
                    package.Workbook.Worksheets["Посещаемость"].Cells[themeName.Address].Value = theme.Theme.Name;

                    var hoursPerVisit = package.Workbook.Names[$"hoursPerVisit{themeVisits}"];
                    package.Workbook.Worksheets["Посещаемость"].Cells[hoursPerVisit.Address].Value = theme.HoursPerVisit;
                    themeVisits++;
                }
            }

            var pageLeft = package.Workbook.Names["pageLeft"];
            package.Workbook.Worksheets["Посещаемость"].Cells[pageLeft.Address].Value = page++;

            var pageRight = package.Workbook.Names["pageRight"];
            package.Workbook.Worksheets["Посещаемость"].Cells[pageRight.Address].Value = page++;


            outputPakage.Workbook.Worksheets.Add(subject.Name + " Cеместр №" + subject.Semester.Number, package.Workbook.Worksheets["Посещаемость"]);
        }

        public string GetThemesList(TypeSubjectEnum typeSubject, int subjectId)
        {
            var themes = _db.Theme
                .Include(x => x.Subject)
                .Include(x => x.ThemeGroup)
                .ToList();

            var filteredThemes = themes.Where(x => (x.Subject.Id == subjectId)
            && (x.Type == typeSubject))
            .ToList();

            string answer = $"<option disabled selected value=''>Не выбрано</option>";
            foreach (var theme in filteredThemes)
            {
                bool disabled = theme.AllowedHours <= theme.ThemeGroup.UsedHours;

                answer += $"<option {(disabled ? "disabled" : "")} " +
                          $"value='{(disabled ? 0 : theme.Id)}'>{theme.Name} - ({theme.ThemeGroup.UsedHours} из {theme.AllowedHours}) ч.</option>";

            }
            return answer;
        }

        /// <summary>
        /// Промежуточное хранение тем
        /// </summary>
        /// <param name="themeId"></param>
        /// <param name="isFirst"></param>
        /// <returns></returns>
        public IActionResult SaveSelectedThemeToSession(int themeId, string allowedTime, bool isFirst)
        {

            if (isFirst) ClearSession();
            var saved = HttpContext.Session.GetString("SessionStorageThemes");

            if (saved == null)
            {

                var theme = _db.Theme.Include(x => x.ThemeGroup).Where(x => x.Id == themeId).FirstOrDefault();
                var themeGroup = new ThemeGroup();
                var usedHours = theme.ThemeGroup.UsedHours;
                var allowedHours = theme.AllowedHours;

                if ((allowedHours - usedHours) >= 2) //можем снять 2 часа
                {
                    themeGroup.Id = theme.ThemeGroup.Id;
                    themeGroup.UsedHours = theme.ThemeGroup.UsedHours + 2;
                    SaveToSession(themeGroup, 2, themeId);
                    return Ok("2");
                }
                else
                {
                    if ((allowedHours - usedHours) > 0) //можем снять < 2 часа
                    {
                        themeGroup.Id = theme.ThemeGroup.Id;
                        var time = Math.Round((allowedHours - usedHours), 1);
                        themeGroup.UsedHours = theme.ThemeGroup.UsedHours + time;
                        SaveToSession(themeGroup, time, themeId);
                        return Ok(time);
                    }
                }

            }
            else
            {

                var themeGroup = new ThemeGroup();

                var theme = _db.Theme.Include(x => x.ThemeGroup).Where(x => x.Id == themeId).FirstOrDefault();
                var usedHours = theme.ThemeGroup.UsedHours;
                var allowedHours = theme.AllowedHours;
                var remaining = Math.Round(allowedHours - usedHours, 1);

                themeGroup.Id = theme.ThemeGroup.Id;
                var allow = Math.Round(Convert.ToDouble(allowedTime), 1);

                if (remaining > allow)
                {
                    themeGroup.UsedHours = Math.Round(theme.ThemeGroup.UsedHours + allow, 1);
                    SaveToSession(themeGroup, allow, themeId);
                    return Ok(allow);
                }
                else
                {
                    themeGroup.UsedHours = Math.Round(theme.ThemeGroup.UsedHours + remaining, 1);
                    SaveToSession(themeGroup, remaining, themeId);
                    return Ok(remaining);
                }

            }
            return Ok();
            void SaveToSession(ThemeGroup themeGroup, double reserved, int themeId)
            {
                var saved = HttpContext.Session.GetString("SessionStorageThemes");
                if (saved == null)
                {
                    var create = new List<SessionStorageThemes>();
                    create.Add(new SessionStorageThemes() { ThemeGroup = themeGroup, Reserved = reserved, ThemeId = themeId });
                    var text = JsonConvert.SerializeObject(create);
                    HttpContext.Session.SetString("SessionStorageThemes", text);
                }
                else
                {
                    var list = JsonConvert.DeserializeObject<List<SessionStorageThemes>>(saved);
                    list.Add(new SessionStorageThemes() { ThemeGroup = themeGroup, Reserved = reserved, ThemeId = themeId });
                    var text = JsonConvert.SerializeObject(list);
                    HttpContext.Session.SetString("SessionStorageThemes", text);
                }
            }
        }

        public string GetThemesListFromSession(TypeSubjectEnum typeSubject, int subjectId)
        {
            var themes = _db.Theme
                .Include(x => x.Subject)
                .Include(x => x.ThemeGroup)
                .ToList();

            var filteredThemes = themes.Where(x => (x.Subject.Id == subjectId)
            && (x.Type == typeSubject))
            .ToList();

            var saved = HttpContext.Session.GetString("SessionStorageThemes");
            var list = new List<SessionStorageThemes>();
            if (saved != null)
            {
                list = JsonConvert.DeserializeObject<List<SessionStorageThemes>>(saved);
            }

            string answer = $"<option disabled selected value=''>Не выбрано</option>";
            foreach (var theme in filteredThemes)
            {
                var savedData = list.Where(x => x.ThemeGroup.Id == theme.ThemeGroup.Id).FirstOrDefault();
                if (savedData is null)
                {
                    bool disabled = theme.AllowedHours <= theme.ThemeGroup.UsedHours;

                    answer += $"<option {(disabled ? "disabled" : "")} " +
                              $"value='{(disabled ? 0 : theme.Id)}'>{theme.Name} - ({theme.ThemeGroup.UsedHours} из {theme.AllowedHours}) ч.</option>";
                }
                else
                {
                    bool disabled = theme.AllowedHours <= savedData.ThemeGroup.UsedHours;

                    answer += $"<option {(disabled ? "disabled" : "")} " +
                              $"value='{(disabled ? 0 : theme.Id)}'>{theme.Name} - ({savedData.ThemeGroup.UsedHours} из {theme.AllowedHours}) ч.</option>";
                }

            }
            return answer;
        }

        public string GetThemeName(int id)
        {
            var theme = _db.Theme.Find(id);
            return $"{theme.Name} ({(TypeSubjectRusEnum)theme.Type})";
        }
        public IActionResult ClearSession()
        {
            HttpContext.Session.Remove("SessionStorageThemes");

            return Ok();
        }
        public IActionResult ClearItemInSession(int id)
        {
            var saved = HttpContext.Session.GetString("SessionStorageThemes");
            var list = new List<SessionStorageThemes>();
            var theme = _db.Theme.Include(x => x.ThemeGroup).Where(x => x.Id == id).FirstOrDefault();
            if (saved != null)
            {
                list = JsonConvert.DeserializeObject<List<SessionStorageThemes>>(saved);

                var remove = list.Where(x => x.ThemeGroup.Id == theme.ThemeGroup.Id).FirstOrDefault();
                list.Remove(remove);

                var text = JsonConvert.SerializeObject(list);
                HttpContext.Session.SetString("SessionStorageThemes", text);
            }
            return Ok();
        }

        public IActionResult IsAllowAddVisit(int subjectId)
        {
            var themes = _db.Theme
                  .Include(x => x.Subject)
                  .Include(x => x.ThemeGroup)
                  .ToList();

            var filteredThemes = themes.Where(x => (x.Subject.Id == subjectId)).ToList();

            var saved = HttpContext.Session.GetString("SessionStorageThemes");
            var list = new List<SessionStorageThemes>();
            if (saved != null)
            {
                list = JsonConvert.DeserializeObject<List<SessionStorageThemes>>(saved);
            }

            var available = 0.0;

            foreach (var theme in filteredThemes)
            {
                available += Math.Round(theme.AllowedHours - theme.ThemeGroup.UsedHours, 1);
            }

            if ((available == list.Sum(x => x.Reserved)) && (list.Count() != 0)) { return Ok(true); };

            if (list.Sum(x => x.Reserved) == 2) { return Ok(true); };

            return Ok(false);
        }

        [HttpPost]
        public IActionResult VisitFilter(GroupVisitView model, string action)
        {

            var visits = _db.GroupVisit
                .Include(x => x.StudentVisits)
                .ThenInclude(x => x.Student)
                .Include(x=>x.StudentVisits)
                .ThenInclude(x=>x.Subject)
                .Include(x => x.Subject)
                .Include(x => x.Group)
                .Include(x => x.ThemeVisits)
                .ThenInclude(x => x.Theme)
                .Where(x => x.Group.Id == model.GroupId).ToList();

            if (action == "reset")
            {
                model.Filter.LastName = default;
                model.Filter.From = visits.Min(x => x.Date);
                model.Filter.Before = visits.Max(x => x.Date);
                model.Visits = visits;
                return View("VisitTableView", model);
            }
            else
            {
                visits = visits.Where(x => (x.Date >= model.Filter.From) && (x.Date <= model.Filter.Before)).ToList();

/*                visits = visits.Where(x => x.Group.Students.Any(y => y.FIO.ToLower().Contains(model.Filter.LastName.ToLower()))).ToList();*/
            }
            var output = new GroupVisitView()
            {
                GroupId = model.GroupId,
                SubjectId = model.SubjectId,
                Visits = visits,
                Filter = model.Filter
            };

            return View("VisitTableView", output);
        }

    }
}
