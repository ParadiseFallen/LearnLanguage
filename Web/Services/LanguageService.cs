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

        public async Task<ApiResponse<Language>> CreateLanguage(Language language)
        {
            Language created = null;
            if (Database.Languages.Where(l => l.CultureInfo == language.CultureInfo).Count() == 0) //no language with this locale
                created = (await Database.AddAsync(language)).Entity;
            await Database.SaveChangesAsync();
            if (created != null)
                return new ApiResponse<Language>() { Content = created };
            return new ApiResponse<Language>() { Errors = new[] { "Language exist." } };
        }
        public async Task<ApiResponse<Language>> GetLanguage(CultureInfo cultureInfo)
        {
            var lang = Database.Languages.Where(l => l.CultureInfo == cultureInfo).FirstOrDefault();
            if (lang != null)
            {
                return new ApiResponse<Language> { Content = lang };
            }
            return new ApiResponse<Language>() { Errors = new[] { "Language dosent exist." } };
        }
        public async Task<ApiResponse<IEnumerable<Language>>> GetAllLanguages() => 
            new ApiResponse<IEnumerable<Language>>()
            { 
                Content = Database.Languages.OrderBy(x => x.Name).ToList() 
            };

        public async Task<ApiResponse<Language>> UpdateLanguage(Language language)
        {
            language = Database.Update(language).Entity;
            await Database.SaveChangesAsync();
            return new ApiResponse<Language>() { Content = language};
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
