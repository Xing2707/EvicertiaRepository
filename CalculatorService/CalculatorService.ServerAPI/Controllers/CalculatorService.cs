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
		private static Logger _serverLogger = LogManager.GetCurrentClassLogger();
		private static string LogInternalError = InternalErrorModel.Error().ErrorMessage.ToString();

		//Url http://localhost:5062/CalculatorService/Add
		[HttpPost("Add")]
		public ActionResult <Addition.AddResponse> Post(Addition.AddRequest request)
		{
			//Create variable,AddressController object usin funtion calculateAdd calculate request return result in variable finaly return result using Addition.AddResponse static function result(integer).
			if (request == null || request.Addends?.Any() != true)
			{
				_serverLogger.Error(LogInternalError);
				return new ObjectResult(InternalErrorModel.Error());
			}

			_serverLogger.Info("Request correct");
			_serverLogger.Info($" Calculate addition {string.Join(',', request.Addends)}");
			var num = AdditionUtils.CalculateAdd(request.Addends);
			_serverLogger.Info($"Result {num}");

			AdditionUtils.SaveAdd(Request.Headers, request.Addends, num);
			return Addition.AddResponse.Result(num);
		}

		[HttpPost("Sub")]
		public ActionResult <Subtraction.SubResponse> Post(Subtraction.SubRequest request)
		{
			if(request == null || (request.Subtrahend == 0 && request.Minuend == 0))
			{
				_serverLogger.Error(LogInternalError);
				return new ObjectResult(InternalErrorModel.Error());
			}

			_serverLogger.Info("Request correct");
			_serverLogger.Info($"Calculate Subtraction {request.Subtrahend},{request.Minuend}");
			var num = SubtractionUtils.CalculateSub(request.Minuend, request.Subtrahend);
			_serverLogger.Info($"Result {num}");

			SubtractionUtils.SaveSub(Request.Headers, request.Minuend, request.Subtrahend, num);

			return Subtraction.SubResponse.Result(num);
		}

		[HttpPost("Mult")]
		public ActionResult <Multiplication.MultResponse> Post(Multiplication.MultRequest request)
		{
			if(request == null || request.Factors?.Any() != true){
				_serverLogger.Error(LogInternalError);
				return new ObjectResult(InternalErrorModel.Error());
			}

			_serverLogger.Info("Request correct");
			_serverLogger.Info($"calculate Multiplication {string.Join(',',request.Factors)}");

			var num =MultiplicationUtils.CalculateMult(request.Factors);
			_serverLogger.Info($"Result {num}");

			MultiplicationUtils.SaveMult(Request.Headers,request.Factors,num);

			return Multiplication.MultResponse.Result(num);
		}

		[HttpPost("Div")]
		public ActionResult <Divide.DivResponse> Post(Divide.DivRequest request)
		{
			const int ZERO = 0;
			if ((request.Divisor == ZERO) || request == null || (request.Dividend == ZERO && request.Divisor == ZERO))
			{
				_serverLogger.Error(LogInternalError);
				return new ObjectResult(InternalErrorModel.Error());
			}

			_serverLogger.Info("Request correct");
			_serverLogger.Info($"calculate Divide {request.Dividend},{request.Divisor}");

			var array = new int[2];
			array = DivideUtils.CalculateDiv(request.Dividend, request.Divisor);
			var quotient = array.First();
			var remainder = array.Last();
			_serverLogger.Info($"Result {quotient} remainder {remainder}");

			DivideUtils.SaveDiv(Request.Headers,request.Dividend,request.Divisor,quotient,remainder);

			return Divide.DivResponse.Result(quotient, remainder);
		}

		[HttpPost("Sqrt")]
		public ActionResult <Square.SqrtResponse> Post(Square.SqrtRequest request)
		{
			if(request == null )
			{
				_serverLogger.Error(LogInternalError);
				return new ObjectResult(InternalErrorModel.Error());
			}

			_serverLogger.Info("Request correct");
			_serverLogger.Info($"Calculate Square {request.Number}");

			var num = SquareUtils.CalculateSqrt(request.Number);
			_serverLogger.Info($"Result {num}");

			SquareUtils.SaveSqrt(Request.Headers, request.Number, num);

			return Square.SqrtResponse.Result(num);
		}

		[HttpPost("Journal/{id}")]

		public ActionResult <Journal.journalResponse> Post(Journal.JournalRequest request)
		{
			const int LENGTH = 5;
			if(request == null || request.Id.Length != LENGTH)
			{
				_serverLogger.Error(LogInternalError);
				return new ObjectResult(InternalErrorModel.Error());
			}

			_serverLogger.Info($"Request correct");
			_serverLogger.Info($"Search Tracking-Id {request.Id}");

			var dictionary = JournalUtils.GetJournalData(request.Id);
			if(dictionary == null){
				_serverLogger.Error("Error! Tracking-Id not found!");
				return Journal.journalResponse.NoIdSelect();
			}else{

				var operation = dictionary.First();
				var calculation = dictionary[1];
				var date = dictionary.Last();
				_serverLogger.Info($"Result {operation};{calculation};{date}");

				return Journal.journalResponse.Result(operation, calculation, date);
			}
		}
	}
}