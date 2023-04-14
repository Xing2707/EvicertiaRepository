using Microsoft.AspNetCore.Mvc;
using NLog;

namespace CalculatorService.ServerAPI.Controllers
{
	public static class SquareUtils
	{
		private static Logger _serverLogger = LogManager.GetCurrentClassLogger();
		public static int CalculateSqrt(int value){
			var num = 0;
			num = ((int)Math.Sqrt(value));

			return num;
		}

		public static void SaveSqrt(IHeaderDictionary headers, int number, int result)
		{
			var key = "X-Evi-Tracking-Id";
			var trakingId = headers[key];

			const String OPERATION = "Sqrt";
			var calculation = "";
			var date = DateTime.Now.ToString();
			var data = new string[3];

			if (trakingId != "xxx")
			{
				_serverLogger.Info($"Find Traking-Id {trakingId}");
				calculation = "√" + number + " = " + result;
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
