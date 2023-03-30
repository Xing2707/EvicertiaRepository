using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace CalculatorService.ServerAPI.Controllers
{
	public class DivideController : Controller
	{
		public int[] CalculateDiv(int dividend, int divisor){
			var result = new int[2];
			result[0] = dividend/divisor;
			result[1] = dividend%divisor;
			return result;
		}
	}
}
