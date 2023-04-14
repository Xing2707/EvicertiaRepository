using Microsoft.AspNetCore.Connections.Features;
using System.Runtime.CompilerServices;

namespace CalculatorService.ServerAPI.Models
{
	public class BadRequestModel
	{
		public string ErrorCode { get; set; }
		public int ErrorStatus { get; set; }
		public string ErrorMessage { get; set; }

		public static BadRequestModel Error(string message)
		{
			const int CODESTATUS = 400;
			const string ERRORCODE = "InternalError";
			const string ERRORMESSAGE = "Unable to process request: ";
			return new BadRequestModel { ErrorCode = ERRORCODE, ErrorStatus = CODESTATUS, ErrorMessage = ERRORMESSAGE + message };
		}
	}
}
