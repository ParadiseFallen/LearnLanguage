using Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServices.ServicesInterfaces
{
    public interface ILanguageService
    {
        public Task<ApiResponse<Language>> GetLanguage(CultureInfo cultureInfo);
        public Task<ApiResponse<IEnumerable<Language>>> GetLanguages();
    }
}
