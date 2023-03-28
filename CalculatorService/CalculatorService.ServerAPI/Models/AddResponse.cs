namespace CalculatorService.ServerAPI.Models
{
	public class AddResponse
	{
		public int Sum { get; set; }

		public static AddResponse AddResult(int value)
		{
			return new AddResponse { Sum = value };
		}
	}
}
