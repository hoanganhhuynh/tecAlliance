using System;
namespace WebApi.Models
{
	public class ErrorResult
	{
        public int ErrorCode { get; }
        public string Message { get; }
        public ErrorResult(int errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message ?? "";
        }
    }
}

