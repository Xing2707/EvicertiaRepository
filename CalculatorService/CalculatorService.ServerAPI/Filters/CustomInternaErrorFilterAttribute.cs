using CalculatorService.ServerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CalculatorService.ServerAPI.Filters
{
	public class CustomInternaErrorFilterAttribute : IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{
			context.Result = new ObjectResult(InternalErrorModel.Error())
			{
				StatusCode = StatusCodes.Status500InternalServerError
			};
		}
	}
}
