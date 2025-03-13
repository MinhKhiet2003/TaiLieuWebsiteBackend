using System.Text.Json.Serialization;

namespace TaiLieuWebsiteBackend.Response
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ErrorMessage { get; set; }

        // Static Success Method
        public static ApiResponse<T> Success(int statusCode, string message, T data)
        {
            return new ApiResponse<T>
            {
                StatusCode = statusCode,
                Message = message,
                Data = data
            };
        }

        // Static Error Method
        public static ApiResponse<T> Error(int statusCode, string message, string errorMessage)
        {
            return new ApiResponse<T>
            {
                StatusCode = statusCode,
                Message = message,
                Data = default,
                ErrorMessage = errorMessage 
            };
        }
    }
}
