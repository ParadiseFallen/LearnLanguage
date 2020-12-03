using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Services
{
    public class LanguageService : DBService
    {
        #region ctor
        public LanguageService(DatabaseContext db) : base(db)
        {
        }
        #endregion

        public async Task<Language> CreateLanguage(Language language)
        {
            Language created = null;
            if (Database.Languages.Where(l=>l.CultureInfo==language.CultureInfo).Count() == 0) //no language with this locale
                created = (await Database.AddAsync(language)).Entity;
            await Database.SaveChangesAsync();
            return created;
        }
        public async Task<Language> GetLanguage(CultureInfo cultureInfo) =>
            Database.Languages.Where(l => l.CultureInfo == cultureInfo).FirstOrDefault();
        public async Task<IEnumerable<Language>> GetAllLanguages()=>
             Database.Languages.OrderBy(x=>x.Name).ToList();
        public async Task<Language> UpdateLanguage(Language language)
        {
            language =  Database.Update(language).Entity;
            await Database.SaveChangesAsync();
            return language;
        }
        public async Task<bool> DeleteLanguage(int languageID)
        {
            var language = Database.Languages.Where(l => l.Id == languageID).FirstOrDefault();
            if (language != default)
            {
                Database.Remove(language);
                await Database.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
