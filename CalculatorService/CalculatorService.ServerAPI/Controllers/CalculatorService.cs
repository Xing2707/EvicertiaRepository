using Microsoft.AspNetCore.Mvc;
using CalculatorService.Library;
using RestSharp;
using System.Text.RegularExpressions;
using CalculatorService.ServerAPI.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using NLog;

namespace CalculatorService.ServerAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CalculatorService : ControllerBase
	{
		private static LogsController logsController = new LogsController();
		private static string LogInternalError = InternalErrorModel.Error().ErrorMessage.ToString();

		//Url http://localhost:5062/CalculatorService/Add
		[HttpPost("Add")]
		public ActionResult <Addition.AddResponse> Post(Addition.AddRequest request)
		{
			//Create variable,AddressController object usin funtion calculateAdd calculate request return result in variable finaly return result using Addition.AddResponse static function result(integer).
			if (request == null || request.Addends?.Any() != true)
			{
				logsController.saveErrorLog(LogInternalError);

				return new ObjectResult(InternalErrorModel.Error());
			}

			logsController.saveInfor("Request correct");
			logsController.saveInfor($" Calculate addition {string.Join(',', request.Addends)}");
			var num = 0;
			var addressController = new AdditionController();
			num = addressController.CalculateAdd(request.Addends);
			logsController.saveInfor($"Result {num}");

			addressController.SaveAdd(Request.Headers, request.Addends, num);
			return Addition.AddResponse.Result(num);
		}

		[HttpPost("Sub")]
		public ActionResult <Subtraction.SubResponse> Post(Subtraction.SubRequest request)
		{
			if(request == null || (request.Subtrahend == 0 && request.Minuend == 0))
			{
				logsController.saveErrorLog(LogInternalError);
				return new ObjectResult(InternalErrorModel.Error());
			}

			logsController.saveInfor("Request correct");
			logsController.saveInfor($"Calculate Subtraction {request.Subtrahend},{request.Minuend}");

			var num = 0;
			var subController = new SubtractioController();
			num = subController.CalculateSub(request.Minuend, request.Subtrahend);
			logsController.saveInfor($"Result {num}");

			subController.SaveSub(Request.Headers, request.Minuend, request.Subtrahend, num);

			return Subtraction.SubResponse.Result(num);
		}

		[HttpPost("Mult")]
		public ActionResult <Multiplication.MultResponse> Post(Multiplication.MultRequest request)
		{
			if(request == null || request.Factors?.Any() != true){
				logsController.saveErrorLog(LogInternalError);
				return new ObjectResult(InternalErrorModel.Error());
			}

			logsController.saveInfor("Request correct");
			logsController.saveInfor($"calculate Multiplication {string.Join(',',request.Factors)}");

			var num = 0;
			var multController = new MultiplicationController();
			num = multController.CalculateMult(request.Factors);
			logsController.saveInfor($"Result {num}");

			multController.SaveMult(Request.Headers,request.Factors,num);

			return Multiplication.MultResponse.Result(num);
		}

		[HttpPost("Div")]
		public ActionResult <Divide.DivResponse> Post(Divide.DivRequest request)
		{
			const int ZERO = 0;
			if ((request.Divisor == ZERO) || request == null || (request.Dividend == ZERO && request.Divisor == ZERO))
			{
				logsController.saveErrorLog(LogInternalError);
				return new ObjectResult(InternalErrorModel.Error());
			}

			logsController.saveInfor("Request correct");
			logsController.saveInfor($"calculate Divide {request.Dividend},{request.Divisor}");

			var array = new int[2];
			var quotient = 0;
			var remainder = 0;
			var divController = new DivideController();
			array = divController.CalculateDiv(request.Dividend, request.Divisor);
			quotient = array.First();
			remainder = array.Last();
			logsController.saveInfor($"Result {quotient} remainder {remainder}");

			divController.SaveDiv(Request.Headers,request.Dividend,request.Divisor,quotient,remainder);

			return Divide.DivResponse.Result(quotient, remainder);
		}

		[HttpPost("Sqrt")]
		public ActionResult <Square.SqrtResponse> Post(Square.SqrtRequest request)
		{
			if(request == null )
			{
				logsController.saveErrorLog(LogInternalError);
				return new ObjectResult(InternalErrorModel.Error());
			}

			logsController.saveInfor("Request correct");
			logsController.saveInfor($"Calculate Square {request.Number}");

			var num = 0;
			var sqrtController = new SquareController();
			num = sqrtController.CalculateSqrt(request.Number);
			logsController.saveInfor($"Result {num}");

			sqrtController.SaveSqrt(Request.Headers, request.Number, num);

			return Square.SqrtResponse.Result(num);
		}

		[HttpPost("Journal/{id}")]

		public ActionResult <Journal.journalResponse> Post(Journal.JournalRequet request)
		{
			const int LENGTH = 5;
			if(request == null || request.Id.Length != LENGTH)
			{
				logsController.saveErrorLog(LogInternalError);
				return new ObjectResult(InternalErrorModel.Error());
			}

			logsController.saveInfor($"Request correct");
			logsController.saveInfor($"Search Tracking-Id {request.Id}");

			var journalController = new JournalController();
			var dictionary = journalController.GetJournalData(request.Id);
			if(dictionary == null){
				logsController.saveErrorLog("Error! Tracking-Id not found!");
				return Journal.journalResponse.NoIdSelect();
			}else{

				var operation = dictionary.First();
				var calculation = dictionary[1];
				var date = dictionary[2];
				logsController.saveInfor($"Result {operation};{calculation};{date}");

				return Journal.journalResponse.Result(operation, calculation, date);
			}
		}
	}
}