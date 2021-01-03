using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Web.Services;

namespace Web.API
{
    [Route("/api/language")]
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class LanguageApi : Controller
    {
        private LanguageService Service { get; }
        public LanguageApi(LanguageService service)
        {
            Service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLanguageAsync([FromBody]Language language)
        {
            var response = await Service.CreateLanguage(language);
            if (response.Content != null)
                return Ok(response);
            return Conflict(response);
        }

        [HttpPut]
        [HttpPatch]
        public async Task<ApiResponse<Language>> UpdateLanguage([FromBody]Language language)=>
            await Service.UpdateLanguage(language);

        [HttpGet("{cultureInfo}")]
        public async Task<ApiResponse<Language>> GetLanguage(CultureInfo cultureInfo) => 
            await Service.GetLanguage(cultureInfo);

        [HttpGet("all")]
        public async Task<ApiResponse<IEnumerable<Language>>> GetAllLanguages() =>
            await Service.GetAllLanguages();

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteLanguage(int id) 
        {
            if (await Service.DeleteLanguage(id))
                return Ok(new ApiResponse<object>());
            return Conflict(new ApiResponse<object>() { Errors = new[] { "Language not exist." } });
        }
    }
}
