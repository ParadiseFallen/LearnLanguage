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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Web.API
{
    [Route("/api/translations")]
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class TranslateApi : Controller
    {
        private TranslationService TranslationService { get; }
        public TranslateApi(TranslationService translationService)
        {
            TranslationService = translationService;
        }


        [HttpGet("{from}/{to}/{count:int=1}")]
        public async Task<IEnumerable<Translation>> GetRandomTranslations
            ([FromRoute] CultureInfo from, [FromRoute] CultureInfo to, int count) =>
            await TranslationService.GetRandomTranslationsAsync(from, to, count);

        [HttpPost]
        public async Task<IActionResult> CreateTranslation([FromBody] Translation translation)
        {
            if ((await TranslationService.CreateTranslation(translation)))
                return Ok();
            return Conflict();
        }

        [HttpPut]
        public async Task<Translation> UpdateTranslation([FromBody] Translation translation) =>
            await TranslationService.UpdateTranslationAsync(translation);

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTranslation(int id)
        {
            if (await TranslationService.DeleteTranslationAsync(id))
                return Ok();
            return Conflict();
        }
    }
}
