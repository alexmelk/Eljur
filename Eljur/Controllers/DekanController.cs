using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eljur.Context;
using Microsoft.AspNetCore.Mvc;

namespace Eljur.Controllers
{
    public class DekanController : Controller
    {
        public dbContext _db;

        public DekanController(dbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }


    }
}
