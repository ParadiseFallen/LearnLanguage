using System.Collections.Generic;

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
        public ApiResponse(IEnumerable<string> errors = null, T content = default) => 
            (Errors, Content) = (errors, content);

        public IEnumerable<string> Errors { get; init; }

        public T Content { get; init; }
    }
}
