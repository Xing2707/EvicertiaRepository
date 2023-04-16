using Microsoft.AspNetCore.Mvc;
using NLog;
using RestSharp;

namespace CalculatorService.ServerAPI.Controllers
{
	public static class DivideUtils
	{
		private static Logger _serverLogger = LogManager.GetCurrentClassLogger();
		public static int[] CalculateDiv(int dividend, int divisor){
			var result = new int[2];
			result[0] = dividend/divisor;
			result[1] = dividend%divisor;
			return result;
		}

		public static void SaveDiv(IHeaderDictionary headers, int dividend, int divisor, int quotient, int remainder)
		{
			var key = "X-Evi-Tracking-Id";
			var trakingId = headers[key];

			const String OPERATION = "Div";
			var calculation ="";
			var date = DateTime.Now.ToString();
			var data = new string[3];

			if (trakingId != "xxx")
			{
				_serverLogger.Info($"Find Traking-Id {trakingId}");
				calculation = dividend + " / " + divisor + " = " + quotient + "\nRestor: " + remainder;
				date = Convert.ToDateTime(date).ToString("yyyy-MM-ddTH:mm:ssZ");

				data[0] = OPERATION;
				data[1] = calculation;
				data[2] = date;

				JournalUtils.SaveJournalData(trakingId, data);
			}else{
				_serverLogger.Info($"Find Traling-Id {trakingId}");
				_serverLogger.Info("Dont save Journal");
			}
		}
	}
}
