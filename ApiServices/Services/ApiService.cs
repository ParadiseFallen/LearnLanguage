using ApiClient;
using ApiServices.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServices.Services
{
    public class ApiService : RestApiServiceBase
    {
        public ApiService(RestClient restClient) : base(restClient)
        {
        }

        public async Task<bool> IsApiAvailable() =>
            await CreateRequest<bool>(GetAsync(Api.ApiBase))
            .SetExceptionHandler(async ex => false)
            .SetResponseHandler(async response => response.IsSuccessStatusCode)
            .ExecuteRequestAsync();
    }
}
