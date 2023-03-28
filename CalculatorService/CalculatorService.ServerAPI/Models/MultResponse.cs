namespace CalculatorService.ServerAPI.Models
{
	public class MultResponse
	{
		public int Product { get; set; }

		public static MultResponse MultResult(int value){
			return new MultResponse { Product = value };
		}
	}
}
