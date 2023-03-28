namespace CalculatorService.ServerAPI.Models
{
	public class SqrtResponse
	{
		public int Square { get; set; }

		public static SqrtResponse ResultSqrt(int value)
		{
			return new SqrtResponse { Square = value};
		}
	}
}
