﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Eljur.Context;
using Eljur.Models;

namespace Eljur.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public UserManager<User> _userManager;
        public dbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, dbContext dbContext, UserManager<User> userManager)
        {
            _logger = logger;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Settings");
        }

        [HttpGet]
        public IActionResult Admin()
        {
            var model = new AdminViewModel
            {
                Users = _dbContext.User.ToList(),
            };
            return View(model);
        }

    }
}
