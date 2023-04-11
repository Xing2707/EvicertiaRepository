using Microsoft.AspNetCore.Mvc;

namespace CalculatorService.ServerAPI.Controllers
{
	public class SubtractioController : Controller
	{
		public int CalculateSub(int minuend, int subtrahend)
		{
			int result;
			result = subtrahend - minuend;
			return Math.Abs(result);
		}

		public void SaveSub(IHeaderDictionary headers, int minuend,int subtrahend, int result)
		{
			var key = "X-Evi-Tracking-Id";
			var trakingId = headers[key];

			const String OPERATION = "Sub";
			var journalController = new JournalController();
			var calculation = "";
			var date = DateTime.Now.ToString();
			var data = new string[3];

			if (trakingId != "xxx")
			{
				calculation = minuend + " - " + subtrahend + " = " + result;
				date = Convert.ToDateTime(date).ToString("yyyy-MM-ddTH:mm:ssZ");

				data[0] = OPERATION;
				data[1] = calculation;
				data[2] = date;

				journalController.SaveJournalData(trakingId, data);
			}
		}
	}
}
