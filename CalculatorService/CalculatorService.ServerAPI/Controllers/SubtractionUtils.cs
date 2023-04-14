using Microsoft.AspNetCore.Mvc;
using NLog;

namespace CalculatorService.ServerAPI.Controllers
{
	public static class SubtractionUtils
	{
		private static Logger _serverLogger = LogManager.GetCurrentClassLogger();
		public static int CalculateSub(int minuend, int subtrahend)
		{
			int result;
			result = subtrahend - minuend;
			return Math.Abs(result);
		}

		public static void SaveSub(IHeaderDictionary headers, int minuend,int subtrahend, int result)
		{
			var key = "X-Evi-Tracking-Id";
			var trakingId = headers[key];

			const String OPERATION = "Sub";
			var calculation = "";
			var date = DateTime.Now.ToString();
			var data = new string[3];

			if (trakingId != "xxx")
			{
				_serverLogger.Info($"Find Traking-Id {trakingId}");
				calculation = minuend + " - " + subtrahend + " = " + result;
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
