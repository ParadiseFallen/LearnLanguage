using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models
{
    /// <summary>
    /// Contains the necessary data to work with response from the server
    /// </summary>
    /// <typeparam name="T">Content type</typeparam>
    /// <remarks>
    /// Immuteble data.
    /// </remarks>
    public record ApiResponse<T> 
    {
        public ApiResponse(){}
        public ApiResponse(IEnumerable<string> errors, T content) => 
            (Errors, Content) = (errors, content);
        public IEnumerable<string> Errors { get; init; }
        public T Content { get; init; }
    }
}
