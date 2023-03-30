using CalculatorService.ServerAPI.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorService.ServerAPI.Filters
{
	//Creat my BadRequest filter attribute
	public class CustomBadRequestFilterAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			if (!context.ModelState.IsValid)
			{
				context.Result = new BadRequestObjectResult(BadRequestModel.error(context.ModelState.Values.First().Errors.First().ErrorMessage));
			}
			base.OnActionExecuting(context);
		}
	}
}