using Microsoft.AspNetCore.Connections.Features;

namespace CalculatorService.ServerAPI.Models
{
	public class InternalErrorModel
	{
		public string ErrorCode { get; set; }
		public int ErrorStatus { get; set; }
		public string ErrorMessage { get; set; }

		public static InternalErrorModel error(string message)
		{
			const string CodeError = "InternalError";
			const int StatusCode = 500;
			const string MessageError = "Unable to process request: ";
			return new InternalErrorModel { ErrorCode = CodeError, ErrorStatus = StatusCode, ErrorMessage = MessageError + message};
		}
	}
}
