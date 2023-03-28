using Microsoft.AspNetCore.Mvc;

namespace CalculatorService.ServerAPI.Controllers
{
	public class SubController : Controller
	{
		public int CalculateSub(int minuend, int subtrahend)
		{
			int result;
			result = subtrahend - minuend;
			return Math.Abs(result);
		}
	}
}
