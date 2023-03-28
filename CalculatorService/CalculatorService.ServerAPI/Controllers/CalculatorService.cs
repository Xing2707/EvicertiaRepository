using Microsoft.AspNetCore.Mvc;
using CalculatorService.Library;
using CalculatorService.ServerAPI.Models;

namespace CalculatorService.ServerAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CalculatorService : ControllerBase
	{

		private static readonly string[] Summaries = new[]
		{
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	};

		private readonly ILogger<CalculatorService> _logger;

		public CalculatorService(ILogger<CalculatorService> logger)
		{
			_logger = logger;
		}


		[HttpPost("Add")]
		public AddResponse Post(AddRequest request)
		{
			var num = 0;
			var addressController = new AddressController();
			num = addressController.calculateAdd(request.Addends);
			return AddResponse.AddResult(num);
		}

		[HttpPost("Sub")]
		public SubResponse Post(SubRequest request)
		{
		var addressController = new AddressController();
			var num = 0;
			var subController = new SubController();
			num = subController.CalculateSub(request.Minuend, request.Subtrahend);
			return SubResponse.SubResult(num);
		}

		[HttpPost("Mult")]
		public MultResponse Post(MultRequest request)
		{
			var num = 0;
			var multController = new MultController();
			num = multController.CalculateMult(request.Factors);
			return MultResponse.MultResult(num);
		}

		[HttpPost("Div")]
		public DivResponse Post(DivRequest request)
		{
			var array = new int[2];
			var quotient = 0;
			var remainder = 0;
			var divController = new DivController();
			array = divController.CalculateDiv(request.Dividend, request.Divisor);
			for(var i = 0; i < array.Length; i++ )
			{
				if(quotient == 0)
				{
					quotient = array[i];
				}else
				{
					remainder = array[i];
				}
			}
			return DivResponse.DivResult(quotient,remainder);
		}
		[HttpPost("Sqrt")]
		public SqrtResponse Post(SqrtRequest request)
		{
			var num = 0;
			var sqrtController = new SqrtResponseController();
			num = sqrtController.CalculateSqrt(request.Number);
			return SqrtResponse.ResultSqrt(num);
		}

		

	}
}