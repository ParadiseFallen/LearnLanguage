using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Services
{
    public class TranslationService : DBService
    {
        public TranslationService(DatabaseContext db) : base(db) { }

        public async Task<IEnumerable<Translation>> GetTranslationsAsync
            (CultureInfo referenceCulture, CultureInfo targetCulture, int count)
            => SelectRandom(Database.Translations, (t) => 
                t.A.Culture == referenceCulture && t.B.Culture == targetCulture ||
                t.B.Culture == referenceCulture && t.A.Culture == targetCulture
            , count)
            //map left side
            .Select(t=> 
            {
                if (t.B.Culture == referenceCulture && t.A.Culture == targetCulture)
                {
                    var temp = t.A;
                    t.A = t.B;
                    t.B = temp;
                }
                return t;
            });

        public async Task AddTranslationAsync()
        { 
            
        }
    }
}
