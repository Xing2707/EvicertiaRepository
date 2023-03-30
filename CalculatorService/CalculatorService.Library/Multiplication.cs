namespace CalculatorService.Library
{
	public class Multiplication
	{
		public class MultRequest
		{
			public int[]? Factors { get; set; }
		}

		public class MultResponse
		{
			public int Product { get; set; }

			public static MultResponse Result(int value)
			{
				return new MultResponse { Product = value };
			}
		}
	}

}
