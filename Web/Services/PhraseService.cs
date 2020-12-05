using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Services
{
    public class PhraseService : DBService
    {
        //public async Task<IEnumerable<Phrase>> GetRandomPhrases(CultureInfo culture, int count)
        //{
        //    var random = new Random();
        //    var filteredPhrases =
        //    DB.Phrases
        //        .Where(p => p.Culture == culture)
        //        .OrderBy(r => random.Next()); //shuffle
        //    var countToTake = count < filteredPhrases.Count() ? count : filteredPhrases.Count();
        //    return filteredPhrases.Take(countToTake);
        //}
        public PhraseService(DatabaseContext db) : base(db)
        {
        }

        public async Task<ApiResponse<object>> CreatePhraseAsync(Phrase phrase)
        {
            if (Database.Phrases.Where(p => p.Text == phrase.Text).Count() != 0)
                return new ApiResponse<object>() { Errors = new[] { "Same phrase exist." } };
            Database.Add(phrase);
            await Database.SaveChangesAsync();
            return new ApiResponse<object>();
        }
        public async Task<ApiResponse<object>> DeletePhraseAsync(int id)
        {
            var phrase = Database.Phrases.Where(p => p.Id == id).FirstOrDefault();
            if (phrase == default)
                return new ApiResponse<object>() {Errors=new[] {"Phrase dosent exist." } };
            Database.Remove(phrase);
            await Database.SaveChangesAsync();
            return new ApiResponse<object>();
        }
        public async Task<ApiResponse<Phrase>> UpdatePhraseAsync(Phrase phrase)
        {
            phrase = Database.Update(phrase).Entity;
            await Database.SaveChangesAsync();
            return new ApiResponse<Phrase>() { Content = phrase};
        }
    }
}
