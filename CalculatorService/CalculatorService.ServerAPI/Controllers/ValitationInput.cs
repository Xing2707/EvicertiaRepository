using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using System.Text.RegularExpressions;

namespace CalculatorService.ServerAPI.Controllers
{
	public class ValitationInput : Controller
	{
		public Boolean TestInput(object value){
			if (value is int){
				return true;
			}else{
				return false;
			}
		}
	}
}
