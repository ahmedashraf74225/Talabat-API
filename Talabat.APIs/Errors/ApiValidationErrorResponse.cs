using System.Collections;

namespace Talabat.APIs.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }

        // validation error is a [Bad Request = 400]
        public ApiValidationErrorResponse():base(400)
        {
            Errors = new List<string>();
        }
    }
}
