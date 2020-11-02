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
    //[Route("/api/phrases")]
    //[Authorize(AuthenticationSchemes = "Identity.Application")]
    //public class PhraseApi : Controller
    //{
    //    public TTSService TTS { get; set; }
    //    public PhraseService PhraseService { get; set; }

    //    public PhraseApi(TTSService tts, PhraseService pService)
    //    {
    //        TTS = tts;
    //        PhraseService = pService;
    //    }


    //    [HttpGet("random/{culture}/{count}")] 
    //    public async Task<Phrase> GetRandomPhrase(CultureInfo culture, int count) => 
    //        PhraseService.GetRandomPhrases(culture, count);
    //    //{
    //    //    return await Task.Run(() => DB.GetRandomPhrase(culture));
    //    //}

    //    [HttpPost]
    //    public async Task<IActionResult> CreatePhrase([FromBody] Phrase phrase)
    //    {
    //        if (DB.Phrases.Where(p => p.Text == phrase.Text).Count() == 0) //check unique
    //        {
    //            var response = await DB.Phrases.AddAsync(phrase);
    //            await DB.SaveChangesAsync();
    //            return Ok("Phrase created");
    //        }
    //        return Ok("phrase with this text exist");
    //    }

    //    [HttpPut]
    //    public async Task<Phrase> UpdatePhrase([FromBody] Phrase phrase)
    //    {
    //        var t = DB.Phrases.Update(phrase).Entity;
    //        await DB.SaveChangesAsync();
    //        return t;
    //    }

    //    [HttpDelete]
    //    public async Task<IActionResult> DeletePhrase([FromBody] Phrase phrase)
    //    {

    //        return Ok();
    //    }
    //}
}
