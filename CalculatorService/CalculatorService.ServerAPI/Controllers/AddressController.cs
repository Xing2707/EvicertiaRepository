using Microsoft.AspNetCore.Mvc;

namespace CalculatorService.ServerAPI.Controllers
{
	public class AddressController : Controller
	{
		public int calculateAdd(int[] values){
			var result = 0;
			foreach(int value in values){
				result += value;
			}
			return result;
		}
	}
}
