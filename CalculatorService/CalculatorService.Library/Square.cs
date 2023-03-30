namespace CalculatorService.Library
{
	public class Square
	{
		public class SqrtRequest
		{
			public int Number { get; set; }
		}

		public class SqrtResponse
		{
			public int Square { get; set; }
			public static SqrtResponse Result(int value)
			{
				return new SqrtResponse { Square = value };
			}
		}
	}
}
