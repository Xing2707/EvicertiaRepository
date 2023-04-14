using Microsoft.AspNetCore.Mvc;
using NLog;

namespace CalculatorService.ServerAPI.Controllers
{
	public static class MultiplicationUtils
	{
		private static Logger _serverLogger = LogManager.GetCurrentClassLogger();
		public static int CalculateMult(int[] values)
		{
			int result = 1;
			foreach(int value in values)
			{
				result *= value;
			}
			return result;
		}

		public static void SaveMult(IHeaderDictionary headers, int[] values, int result)
		{
			var key = "X-Evi-Tracking-Id";
			var trakingId = headers[key];

			const String OPERATION = "Mult";
			var calculation = "";
			var date = DateTime.Now.ToString();
			var data = new string[3];

			if (trakingId != "xxx")
			{
				_serverLogger.Info($"Find Traking-Id {trakingId}");
				calculation = string.Join(" * ", values) + " = " + result;
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
