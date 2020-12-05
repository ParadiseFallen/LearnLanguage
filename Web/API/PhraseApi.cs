using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Models;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using Web.Services;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

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
        public async Task<ApiResponse<Phrase>> UpdatePhraseAsync([FromBody]Phrase phrase) => 
            await PhraseService.UpdatePhraseAsync(phrase);
        
        [HttpPut]
        public async Task<IActionResult> CreatePhraseAsync([FromBody] Phrase phrase) 
        {
            var response = await PhraseService.CreatePhraseAsync(phrase);
            if (response.Errors.Count()==0)
                return Ok(response);
            return Conflict(response);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePhraseAsync(int id)
        {
            var response = await PhraseService.DeletePhraseAsync(id);
            if (response.Errors.Count() == 0)
                return Ok(response);
            return Conflict(response);
        }

    }
}
