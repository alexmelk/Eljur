using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eljur.Context;
using Eljur.Context.Tables;
using Eljur.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eljur.Controllers
{
    public class SharedController : Controller
    {
        public dbContext _db;

        public SharedController(dbContext db)
        {
            _db = db;
        }

        public IActionResult ChooseGroupForComments()
        {
            return View(new Semester());
        }

        [HttpPost]
        public IActionResult ChooseGroupForComments(Semester semester)
        {

            var sem = _db.Semesters.Include(x => x.Comments).Where(x => x.Id == semester.Id).FirstOrDefault();
            while(sem.Comments.Count()!=14)
            {
                sem.Comments.Add(new Comment() { Semester = _db.Semesters.Find(semester.Id), DekanDescription = "", TeacherDescription ="" });
                _db.SaveChanges();
            }
            var teachDesc = sem.Comments.Select(x => x.TeacherDescription).ToList();
            var dekanDesc = sem.Comments.Select(x => x.DekanDescription).ToList();

            return View("CommentsView",new CommentsModel { TeacherDescriptions = teachDesc, DekanDescriptions = dekanDesc, SemesterId = sem.Id });
        }

        [HttpPost]
        public IActionResult EditComments(CommentsModel comments)
        {
            var sem = _db.Semesters.Include(x => x.Comments).Where(x => x.Id == comments.SemesterId).FirstOrDefault();

            for (int i = 0; i < comments.DekanDescriptions?.Count(); i++)
            {
                sem.Comments[i].DekanDescription =  comments.DekanDescriptions[i];
            }
            for (int i = 0; i < comments.TeacherDescriptions?.Count(); i++)
            {
                sem.Comments[i].TeacherDescription = comments.TeacherDescriptions[i];
            }
            _db.SaveChanges();

            if (User.IsInRole("admin")) { return RedirectToAction("Index", "Settings"); }
            if (User.IsInRole("dekan")) { return RedirectToAction("Index", "Dekan"); }
            if (User.IsInRole("teacher")) { return RedirectToAction("Index", "Settings"); }


            return RedirectToAction("Index");
        }




    }
}
