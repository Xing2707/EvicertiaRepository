namespace CalculatorService.Library
{
	public class Addition{
		public class AddRequest
		{
			public int[]? Addends { get; set; }

		}
		public class AddResponse
		{
			public int Sum { get; set; }

			public static AddResponse result(int value) {
				return new AddResponse { Sum = value };
			}
		}
	}
}
