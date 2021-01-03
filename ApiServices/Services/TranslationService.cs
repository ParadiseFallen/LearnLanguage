using ApiClient;
using ApiServices.Endpoints;
using ApiServices.ServicesInterfaces;
using Models;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace ApiServices.Services
{
    public class TranslationService : RestApiServiceBase, ITranslationService
    {
        public TranslationService(RestClient restClient) : base(restClient) { }

        public Task<ApiResponse<IEnumerable<Translation>>> GetRandomTranslations
            (CultureInfo from, CultureInfo to, int count = 1) =>
            CreateRequest<ApiResponse<IEnumerable<Translation>>>(GetAsync(Api.Translation.CompileGetTranslations(from, to, count)))
            .ExecuteRequestAsync();
    }
}
