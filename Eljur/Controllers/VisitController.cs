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
            return View("Index");
        }
    }
}
