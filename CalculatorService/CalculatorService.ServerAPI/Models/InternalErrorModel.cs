using Microsoft.AspNetCore.Connections.Features;

namespace CalculatorService.ServerAPI.Models
{
	public class InternalErrorModel
	{
		public string ErrorCode { get; set; }
		public int ErrorStatus { get; set; }
		public string ErrorMessage { get; set; }

		public static InternalErrorModel Error()
		{
			const string CodeError = "InternalError";
			const int StatusCode = 500;
			const string MessageError = "An unexpected error condition was triggered which made impossible to fulfill the request. Please try again or contact support.";
			return new InternalErrorModel { ErrorCode = CodeError, ErrorStatus = StatusCode, ErrorMessage = MessageError};
		}
	}
}
