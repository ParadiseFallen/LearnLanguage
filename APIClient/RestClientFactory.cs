using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient
{
    public class RestClientFactory
    {

        public RestClientFactory()
        {

        }

        public RestClient Create()
        {
            var handler = new HttpClientHandler();
            return new RestClient(handler);
        }

    }
}
