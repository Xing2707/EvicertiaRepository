using Microsoft.AspNetCore.Mvc;
using NLog;
using System;

namespace CalculatorService.ServerAPI.Controllers
{
	public static class AdditionUtils
	{
		private static Logger _serverLogger = LogManager.GetCurrentClassLogger();
		public static int CalculateAdd(int[] values)
		{
			var result = 0;
			foreach(int value in values)
			{
				result += value;
			}
			return result;
		}

		public static void SaveAdd(IHeaderDictionary headers, int[] values, int result)
		{
			var key = "X-Evi-Tracking-Id";
			var trakingId = headers[key];
			const String OPERATION = "Sum";
			var calculation = "";
			var date = DateTime.Now.ToString();
			var data = new string[3];

			if (trakingId != "xxx")
			{
				_serverLogger.Info($"Find Traking-Id {trakingId}");
				calculation = string.Join(" + ", values) + " = " + result;
				date = Convert.ToDateTime(date).ToString("yyyy-MM-ddTH:mm:ssZ");
				data[0] = OPERATION;
				data[1] = calculation;
				data[2] = date;
				JournalUtils.SaveJournalData(trakingId, data);
			}
			else
			{
				_serverLogger.Info($"Find Traling-Id {trakingId}");
				_serverLogger.Info("Dont save Journal");
			}
		}
	}
}
