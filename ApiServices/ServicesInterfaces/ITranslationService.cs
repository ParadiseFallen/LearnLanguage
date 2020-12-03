using Models;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ApiServices.ServicesInterfaces
{
    public interface ITranslationService
    {
        Task<ApiResponse<IEnumerable<Translation>>> GetRandomTranslations(CultureInfo from, CultureInfo to, int count = 1);
    }
}
