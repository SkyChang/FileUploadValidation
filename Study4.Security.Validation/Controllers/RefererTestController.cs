using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Study4.Security.Utility.Attributes;

namespace Study4.Security.Utility.Test.Controllers
{
    public class RefererTestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [RefererCheckAttribute]
        public IActionResult TestRefererCheck()
        {
            return View();
        }

       
    }
}