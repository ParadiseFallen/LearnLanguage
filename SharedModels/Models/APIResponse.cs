using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    /// <summary>
    /// Contains the necessary data to work with response from the server
    /// </summary>
    /// <typeparam name="T">Content type</typeparam>
    /// <remarks>
    /// Immuteble data.
    /// </remarks>
    public record APIResponse<T>
    {
        public APIResponse(IEnumerable<string> errors = null, T content = default) => (Errors, Content) = (errors, content);
        public IEnumerable<string> Errors { get; init; }
        public T Content { get; init; }
    }
}
