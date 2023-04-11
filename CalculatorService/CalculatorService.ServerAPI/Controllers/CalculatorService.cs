using Microsoft.AspNetCore.Mvc;
using CalculatorService.Library;
using RestSharp;
using System.Text.RegularExpressions;
using CalculatorService.ServerAPI.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

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
		public ActionResult <Addition.AddResponse> Post(Addition.AddRequest request)
		{
			//Create variable,AddressController object usin funtion calculateAdd calculate request return result in variable finaly return result using Addition.AddResponse static function result(integer).
			if(request == null)
			{
				return new ObjectResult(InternalErrorModel.Error());
			}
			var num = 0;
			var addressController = new AdditionController();
			num = addressController.CalculateAdd(request.Addends);

			addressController.SaveAdd(Request.Headers, request.Addends, num);

			return Addition.AddResponse.result(num);
		}

		[HttpPost("Sub")]
		public ActionResult <Subtraction.SubResponse> Post(Subtraction.SubRequest request)
		{
			if(request == null)
			{
				return new ObjectResult(InternalErrorModel.Error());
			}
			var num = 0;
			var subController = new SubtractioController();
			num = subController.CalculateSub(request.Minuend, request.Subtrahend);

			subController.SaveSub(Request.Headers, request.Minuend, request.Subtrahend, num);

			return Subtraction.SubResponse.Result(num);
		}

		[HttpPost("Mult")]
		public ActionResult <Multiplication.MultResponse> Post(Multiplication.MultRequest request)
		{
			if(request == null){
				return new ObjectResult(InternalErrorModel.Error());
			}
			var num = 0;
			var multController = new MultiplicationController();
			num = multController.CalculateMult(request.Factors);

			multController.SaveMult(Request.Headers,request.Factors,num);

			return Multiplication.MultResponse.Result(num);
		}

		[HttpPost("Div")]
		public ActionResult <Divide.DivResponse> Post(Divide.DivRequest request)
		{
			const int ZERO = 0;
			if ((request.Divisor == ZERO) || request == null)
			{
				return new ObjectResult(InternalErrorModel.Error());
			}
			var array = new int[2];
			var quotient = 0;
			var remainder = 0;
			var divController = new DivideController();
			array = divController.CalculateDiv(request.Dividend, request.Divisor);
			quotient = array.First();
			remainder = array.Last();

			divController.SaveDiv(Request.Headers,request.Dividend,request.Divisor,quotient,remainder);

			return Divide.DivResponse.Result(quotient, remainder);
		}

		[HttpPost("Sqrt")]
		public ActionResult <Square.SqrtResponse> Post(Square.SqrtRequest request)
		{
			const int ZERO = 0;
			if(request == null)
			{
				return new ObjectResult(InternalErrorModel.Error());
			}
			var num = 0;
			var sqrtController = new SquareController();
			num = sqrtController.CalculateSqrt(request.Number);

			sqrtController.SaveSqrt(Request.Headers, request.Number, num);

			return Square.SqrtResponse.Result(num);
		}

		[HttpPost("Journal/{id}")]

		public ActionResult <Journal.journalResponse> Post(Journal.JournalRequet request)
		{
			const int LENGTH = 5;
			if(request == null || request.Id.Length != LENGTH)
			{
				return new ObjectResult(InternalErrorModel.Error());
			}
			var journalController = new JournalController();
			var dictionary = journalController.GetJournalData(request.Id);
			if(dictionary == null){
				return Journal.journalResponse.NoIdSelect();
			}else{
				var operation = dictionary.First();
				var calculation = dictionary[1];
				var date = dictionary[2];
				return Journal.journalResponse.Result(operation, calculation, date);
			}
		}
	}
}