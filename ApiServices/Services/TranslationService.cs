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
    public class TranslationService : RestApiServiceBase, ITranslationService
    {
        public TranslationService(RestClient restClient) : base(restClient) { }

        public Task<ApiResponse<IEnumerable<Translation>>> GetRandomTranslations
            (CultureInfo from, CultureInfo to, int count = 1) =>
            ExecuteRequestAsync<ApiResponse<IEnumerable<Translation>>>(GetAsync(Api.Translation.CompileGetTranslations(from,to,count)));
    }
}
