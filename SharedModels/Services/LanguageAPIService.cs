using Models.Models;
using Models.Services.API;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Models.Services
{
    public class LanguageAPIService : APIServiceBase
    {
        public LanguageAPIService(HttpApiClient httpApiClient, JsonSerializerOptions serializerOptions) : base(httpApiClient, serializerOptions)
        {
        }

        public async Task<Language> GetLanguage(CultureInfo cultureInfo)
        {
            return await Http.Client.GetFromJsonAsync<Language>(APIEndpoints.CompileGetLanguageEndpoint(cultureInfo));
        }
        public async Task<IEnumerable<Language>> GetLanguages()
        {
            try
            {
                return await Http.Client.GetFromJsonAsync<IEnumerable<Language>>(APIEndpoints.GetLanguagesEndpoint,SerializerOptions);
            }
            catch (System.Exception ex)
            {
                return new List<Language>();
            }
        }

    }
}
