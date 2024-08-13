using System;
namespace E_Commerce.Business.Errors
{
	public class ApiException
	{
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
        public ApiException(int statusCode, string message = null, string detail = null)
        {
            StatusCode = statusCode;
            Message = message;
            Detail = detail;
        }
    }
}

