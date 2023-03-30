using Microsoft.AspNetCore.Mvc;
using CalculatorService.Library;
using RestSharp;
using System.Text.RegularExpressions;

namespace CalculatorService.ServerAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CalculatorService : ControllerBase
	{
		private readonly ILogger<CalculatorService> _logger;

		public CalculatorService(ILogger<CalculatorService> logger)
		{
			_logger = logger;
		}

		//Url http://localhost:5062/CalculatorService/Add
		[HttpPost("Add")]
		public Addition.AddResponse Post(Addition.AddRequest request)
		{
			//Create variable,AddressController object usin funtion calculateAdd calculate request return result in variable finaly return result using Addition.AddResponse static function result(integer).
			var num = 0;
			var addressController = new AdditionController();
			var headers = Request.Headers;
			num = addressController.CalculateAdd(request.Addends);

			addressController.SaveAdd(headers,request.Addends,num);

			return Addition.AddResponse.result(num);
		}

		[HttpPost("Sub")]
		public Subtraction.SubResponse Post(Subtraction.SubRequest request)
		{
			var num = 0;
			var subController = new SubtractioController();
			num = subController.CalculateSub(request.Minuend, request.Subtrahend);
			return Subtraction.SubResponse.Result(num);
		}

		[HttpPost("Mult")]
		public Multiplication.MultResponse Post(Multiplication.MultRequest request)
		{
			var num = 0;
			var multController = new MultiplicationController();
			num = multController.CalculateMult(request.Factors);
			return Multiplication.MultResponse.Result(num);
		}

		[HttpPost("Div")]
		public Divide.DivResponse Post(Divide.DivRequest request)
		{
			var array = new int[2];
			var quotient = 0;
			var remainder = 0;
			var divController = new DivideController();
			array = divController.CalculateDiv(request.Dividend, request.Divisor);
			for (var i = 0; i < array.Length; i++)
			{
				if (quotient == 0)
				{
					quotient = array[i];
				} else
				{
					remainder = array[i];
				}
			}
			return Divide.DivResponse.Result(quotient, remainder);
		}

		[HttpPost("Sqrt")]
		public Square.SqrtResponse Post(Square.SqrtRequest request)
		{
			var num = 0;
			var sqrtController = new SquareController();
			num = sqrtController.CalculateSqrt(request.Number);
			return Square.SqrtResponse.Result(num);
		}

		[HttpPost("Journal/{id}")]

		public Journal.journalResponse Post(Journal.JournalRequet requet)
		{
			var journalController = new JournalController();
			var dictionary = journalController.GetJournalData(requet.Id);
			var operation = dictionary[0];
			var calculation = dictionary[1];
			var date = dictionary[2];
			return Journal.journalResponse.Result(operation, calculation, date);
		}
	}
}