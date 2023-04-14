using Microsoft.AspNetCore.Mvc;

namespace CalculatorService.ServerAPI.Controllers
{
	public class MultiplicationController : Controller
	{
		public int CalculateMult(int[] values)
		{
			int result = 1;
			foreach(int value in values)
			{
				result *= value;
			}
			return result;
		}

		public void SaveMult(IHeaderDictionary headers, int[] values, int result)
		{
			var key = "X-Evi-Tracking-Id";
			var trakingId = headers[key];

			const String OPERATION = "Mult";
			var journalController = new JournalController();
			var logController = new LogsController();
			var calculation = "";
			var date = DateTime.Now.ToString();
			var data = new string[3];

			if (trakingId != "xxx")
			{
				logController.saveInfor($"Find Traking-Id {trakingId}");
				calculation = string.Join(" * ", values) + " = " + result;
				date = Convert.ToDateTime(date).ToString("yyyy-MM-ddTH:mm:ssZ");

				data[0] = OPERATION;
				data[1] = calculation;
				data[2] = date;

				journalController.SaveJournalData(trakingId, data);
			}else{
				logController.saveInfor($"Find Traling-Id {trakingId}");
				logController.saveInfor("Dont save Journal");
			}
		}
	}
}
