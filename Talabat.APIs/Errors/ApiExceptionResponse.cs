namespace Talabat.APIs.Errors
{
    public class ApiExceptionResponse : ApiResponse
    {
        public string? Details { get; set; }

        public ApiExceptionResponse(int statusCode ,string ? msg=null ,string? details = null):base(statusCode,msg)
        {
            Details = details;
        }
    }
}
