using System;
using System.Linq;
using System.Text.Json;
using Models;
namespace LocalTests
{
    class Program
    {
        static void Main(string[] args)
        {

            var userInfo = new UserInfo() { Email = "testmail", Username = "testName" };
            //var response = new ApiResponse<UserInfo>() { Content = userInfo };
            var response = new ApiResponse<UserInfo>() { Errors = new[] { "fuck" } };


            var serializerOption = new JsonSerializerOptions();
            serializerOption.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            JsonExtension.GetAllConverters().ToList().ForEach(serializerOption.Converters.Add);

            var text = JsonSerializer.Serialize(response, serializerOption);
            Console.WriteLine($"test : {text}");



            var actual = JsonSerializer.Deserialize<ApiResponse<UserInfo>>(text, serializerOption);
            Console.WriteLine(actual.Content.Username);
        }
    }
}
