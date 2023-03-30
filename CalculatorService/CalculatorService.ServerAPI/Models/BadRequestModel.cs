using Microsoft.AspNetCore.Connections.Features;

namespace CalculatorService.ServerAPI.Models
{
	public class BadRequestModel
	{
		public string ErrorCode { get; set; }
		public int ErrorStatus { get; set; }
		public string ErrorMessage { get; set; }

		public static BadRequestModel error(string message)
		{
			const int CodeStatus = 400;
			const string CodeError = "InternalError";
			const string MessageError = "An unexpected error condition was triggered which made inpossible to fulfill the request. Please try again or contact suppert";
			return new BadRequestModel { ErrorCode = CodeError, ErrorStatus = CodeStatus, ErrorMessage = MessageError };
		}
	}
}
