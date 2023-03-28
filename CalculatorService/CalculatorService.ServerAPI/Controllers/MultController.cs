using Microsoft.AspNetCore.Mvc;

namespace CalculatorService.ServerAPI.Controllers
{
	public class MultController : Controller
	{
		public int CalculateMult(int[] values){
			int result = 1;
			foreach(int value in values){
				result *= value;
			}
			return result;
		}
	}
}
