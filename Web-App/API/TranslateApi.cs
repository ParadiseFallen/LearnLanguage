using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharedModels.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using Web.Services;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;


namespace Web.API
{
    [Route("/api/translator")]
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class TranslateApi : Controller
    {
        [HttpGet()]
        public async Task<Translation> GetTranslations() 
        {
            return default;
        }

    }
}
