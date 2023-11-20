namespace Talabat.APIs.Errors
{
    public class ApiResponse    
    {
        public int StatusCode { get; set; }

        public string? Message { get; set; }

        public ApiResponse(int statusCode , string ? message = null)
        {
            StatusCode = statusCode ;
            Message = message?? getDefaultMessage(statusCode);
        }

        private string? getDefaultMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request",
                401 => "UnAuthorized",
                404 => "Resource Not Found",
                500 => "Server Error",
                _ => null
            };
        }
    }
}
