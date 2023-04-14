namespace CalculatorService.Library
{
	public class Divide
	{
		public class DivRequest
		{
			public int Dividend { get; set; }
			public int Divisor { get; set; }
		}

		public class DivResponse
		{
			public int Quotient { get; set; }
			public int Remainder { get; set; }

			public static DivResponse Result(int value1, int value2)
			{
				return new DivResponse { Quotient = value1, Remainder = value2 };
			}
		}
	}
}
