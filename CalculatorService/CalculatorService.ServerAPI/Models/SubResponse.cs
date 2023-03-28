using System.ComponentModel;

namespace CalculatorService.ServerAPI.Models
{
	public class SubResponse
	{
		public int Difference { get; set; }

		public static SubResponse SubResult(int value)
		{
			return new SubResponse { Difference = value };
		}
	}
}
