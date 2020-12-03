using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Web.Services;

namespace Web.API
{
    [Route("/api/language")]
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class LanguageAPI : Controller
    {
        private LanguageService Service { get; }
        public LanguageAPI(LanguageService service)
        {
            Service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLanguageAsync([FromBody] Language language)
        {
            language = await Service.CreateLanguage(language);
            if (language!= null)
                return Ok(language);
            return Conflict("Language exist.");
        }

        [HttpGet("{cultureInfo}")]
        public async Task<Language> GetLanguage(CultureInfo cultureInfo) => 
            await Service.GetLanguage(cultureInfo);

        [HttpGet("all")]
        public async Task<IEnumerable<Language>> GetAlllanguages() =>
            await Service.GetAllLanguages();

        [HttpPut]
        public async Task<Language> UpdateLanguage(Language language) =>
            await Service.UpdateLanguage(language);

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteLanguage(int id) 
        {
            if (await Service.DeleteLanguage(id))
                return Ok();
            return Conflict("Language not exist.");
        }
        //[Route("/Error")]
        //public async Task<ApiResponse<Language>> Error()=>
    }
}
