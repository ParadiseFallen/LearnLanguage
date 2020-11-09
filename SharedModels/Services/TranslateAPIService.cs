using Models.Services.API;
using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Models.Services.API
{
    public class TranslateAPIService : APIServiceBase
    {
        public TranslateAPIService(HttpApiClient httpApiClient, JsonSerializerOptions serializerOptions) : base(httpApiClient, serializerOptions)
        {
        }

        public async Task<IEnumerable<Translation>> GetRandomTranslations(CultureInfo from,CultureInfo to, int count = 1)
            => await Http.Client.GetFromJsonAsync<IEnumerable<Translation>>(
                APIEndpoints.CompileGetTranslationsEndpoint(from,to,count)
                ,SerializerOptions);
        public async Task<Translation> CreateTranslation(Translation translation) => 
            await (await Http.Client.PostAsJsonAsync<Translation>(
                APIEndpoints.CreateTranslationEndpoint,translation
                , SerializerOptions)).Content.ReadFromJsonAsync<Translation>(SerializerOptions);
        //add delete and update
    }
}
