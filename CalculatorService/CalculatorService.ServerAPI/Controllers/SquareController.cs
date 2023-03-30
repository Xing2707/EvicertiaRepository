using Microsoft.AspNetCore.Mvc;

namespace CalculatorService.ServerAPI.Controllers
{
	public class SquareController : Controller
	{
		public int CalculateSqrt(int value){
			var num = 0;
			num = ((int)Math.Sqrt(value));

			return num;
		}
	}
}
