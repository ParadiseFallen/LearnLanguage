using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_App.API
{
    [Route("/api")]
    public class StatusAPI : Controller
    {
        [HttpGet]
        public IActionResult IsOnline() => Ok();
    }
}
