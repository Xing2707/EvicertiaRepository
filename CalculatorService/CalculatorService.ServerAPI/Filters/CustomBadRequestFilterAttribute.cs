using CalculatorService.ServerAPI.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using CalculatorService.ServerAPI.Controllers;

namespace CalculatorService.ServerAPI.Filters
{
	//Creat my BadRequest filter attribute
	public class CustomBadRequestFilterAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			if (!context.ModelState.IsValid)
			{
				var logsControlle = new LogsController();
				logsControlle.saveErrorLog(context.ModelState.Values.First().Errors.First().ErrorMessage);
				context.Result = new BadRequestObjectResult(BadRequestModel.Error(context.ModelState.Values.First().Errors.First().ErrorMessage));
			}
			base.OnActionExecuting(context);
		}
	}
}