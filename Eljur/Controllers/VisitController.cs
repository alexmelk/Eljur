using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eljur.Context;
using Eljur.Models;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult ChooseTime(ChoosePropertyVisit model)
        {
            return View("VisitsView");
        }
    }
}
