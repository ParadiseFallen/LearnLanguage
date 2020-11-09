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
using Microsoft.AspNetCore.Authorization;

namespace Web.API
{
    [Route("/api/phrases")]
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class PhraseApi : Controller
    {
        private TTSService TTS { get;  }
        private PhraseService PhraseService { get;  }

        public PhraseApi(PhraseService phraseService,TTSService tts)
        {
            PhraseService = phraseService;
        }

        [HttpPost]
        public async Task<Phrase> UpdatePhraseAsync([FromBody]Phrase phrase)
        {
            phrase = await PhraseService.UpdatePhraseAsync(phrase);
            return phrase;
        }
        [HttpPut]
        public async Task<IActionResult> CreatePhraseAsync([FromBody] Phrase phrase) 
        {
            if (await PhraseService.CreatePhraseAsync(phrase))
                return Ok();
            return Conflict();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePhraseAsync(int id)
        {
            if (await PhraseService.DeletePhraseAsync(id))
                return Ok();
            return Conflict();
        }

    }
}
