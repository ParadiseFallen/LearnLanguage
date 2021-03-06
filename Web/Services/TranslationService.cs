﻿using Microsoft.EntityFrameworkCore;
using Models;
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
        #region Ctor
        public TranslationService(DatabaseContext db) : base(db) { }
        #endregion

        #region GET
        //public async Task<IEnumerable<Translation>> GetRandomTranslationsAsync
        //    (CultureInfo referenceCulture, CultureInfo targetCulture, int count)
        //    => SelectRandom(Database.Translations.Include(x=>x.A).Include(x=>x.B), (t) => 
        //        t.A.Culture == referenceCulture && t.B.Culture == targetCulture ||
        //        t.B.Culture == referenceCulture && t.A.Culture == targetCulture
        //    , count)
        //    //map left side
        //    .Select(t=> 
        //    {
        //        if (t.B.Culture == referenceCulture && t.A.Culture == targetCulture)
        //        {
        //            var temp = t.A;
        //            t.A = t.B;
        //            t.B = temp;
        //        }
        //        return t;
        //    });

        public async Task<ApiResponse<IEnumerable<Translation>>> GetRandomTranslationsAsync
            (CultureInfo targetCulture, CultureInfo referenceCulture,  int count)
        {
            var random = new Random();
            var filtered = Database.Translations.Include(x => x.A).ThenInclude(x=>x.Culture).Include(x => x.B).ThenInclude(x=>x.Culture).AsEnumerable()
                                .Where(t =>
                t.A.Culture.CultureInfo.Equals(referenceCulture) && t.B.Culture.CultureInfo.Equals(targetCulture) ||
                t.B.Culture.CultureInfo.Equals(referenceCulture) && t.A.Culture.CultureInfo.Equals(targetCulture))
                                .OrderBy(r => random.Next()); //shuffle
            var countToTake = count < filtered.Count() ? count : filtered.Count();
            var mappedTranslations =  filtered.Take(countToTake).ToList().Select(t=> 
            {
                if (t.B.Culture.CultureInfo.Equals(referenceCulture) && t.A.Culture.CultureInfo.Equals(targetCulture))
                {
                    var temp = t.A;
                    t.A = t.B;
                    t.B = temp;
                }
                return t;
            });
            return new ApiResponse<IEnumerable<Translation>>() { Content = mappedTranslations };
        }

        //public async Task<Translation> GetRandomTranslationAsync(CultureInfo referenceCulture, CultureInfo targetCulture) => 
        //    (await GetRandomTranslationsAsync(referenceCulture,targetCulture,1)).FirstOrDefault();

        #endregion

        #region Update
        public async Task<ApiResponse<Translation>> UpdateTranslationAsync(Translation translation)
        {
            translation = Database.Translations.Update(translation).Entity;
            await Database.SaveChangesAsync();
            return new ApiResponse<Translation>() { Content= translation };
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteTranslationAsync(int id)
        {
            var target = Database.Translations.Where(x => x.Id == id).FirstOrDefault();
            if (target == default)
                return false;
            Database.Translations.Remove(target);
            await Database.SaveChangesAsync();
            return true;
        }
        #endregion

        #region Create
        public async Task<bool> CreateTranslation(Translation translation)
        {
            if (Database
                .Translations
                .Where(x => x.Equals(translation))
                .Count() != 0)
                return false;

            var result = await Database.Translations.AddAsync(translation);
            await Database.SaveChangesAsync();

            return true;
        }
        #endregion
    }
}
