namespace CalculatorService.ServerAPI.Models
{
	public class DivResponse
	{
		public int Quotient { get; set; }
		public int Remainder { get; set; }

		public static DivResponse DivResult(int value1, int value2){
			return new DivResponse { Quotient = value1, Remainder = value2 };
		}
	}
}
