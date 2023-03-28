namespace CalculatorService.ServerAPI.Models
{
	public class BadRequest
	{
		public string ErrorCode {get;set;}
		public int ErrorStatus { get;set;}
		public string ErrorMessage { get;set;}

		public static BadRequest error()
		{
			const int status = 400;
			return new BadRequest
			{
				ErrorCode = "InternalError",
				ErrorStatus = status,
				ErrorMessage = "Unable to process request"
			};
		}
	}
}
