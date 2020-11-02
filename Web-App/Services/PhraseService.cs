using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Services
{
    public class PhraseService
    {
        public DatabaseContext DB { get; set; }
        public PhraseService(DatabaseContext db)
        {
            DB = db;
        }

        public async Task<IEnumerable<Phrase>> GetRandomPhrases(CultureInfo culture, int count)
        {
            var random = new Random();
            var filteredPhrases =
            DB.Phrases
                .Where(p => p.Culture == culture)
                .OrderBy(r => random.Next()); //shuffle
            var countToTake = count < filteredPhrases.Count() ? count : filteredPhrases.Count();
            return filteredPhrases.Take(countToTake);
        }
    }
}
