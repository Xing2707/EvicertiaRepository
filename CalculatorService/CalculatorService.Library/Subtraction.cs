using System.Net;

namespace CalculatorService.Library
{
	public class Subtraction
	{
		public class SubRequest
		{
			public int Minuend { get; set; }
			public int Subtrahend { get; set; }
		}

		public class SubResponse {
			public int Difference { get; set; }

			public static SubResponse Result(int value)
			{
				return new SubResponse { Difference = value };
			}
		}

	}
}
