using ApiClient;
using ApiServices.Endpoints;
using ApiServices.ServicesInterfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServices.Services
{
    public class LanguageService : RestApiServiceBase, ILanguageService
    {
        public LanguageService(RestClient restClient) : base(restClient)
        {
        }

        public Task<ApiResponse<Language>> GetLanguage(CultureInfo cultureInfo) =>
            CreateRequest<ApiResponse<Language>>(GetAsync(Api.Language.CompileGetLanguage(cultureInfo)))
            .ExecuteRequestAsync();

        public Task<ApiResponse<IEnumerable<Language>>> GetLanguages() =>
            CreateRequest<ApiResponse<IEnumerable<Language>>>(GetAsync(Api.Language.AllLanguages))
            .ExecuteRequestAsync();
    }
}
