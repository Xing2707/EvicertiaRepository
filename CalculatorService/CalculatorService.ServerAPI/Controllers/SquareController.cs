using Microsoft.AspNetCore.Mvc;

namespace CalculatorService.ServerAPI.Controllers
{
	public class SquareController : Controller
	{
		public int CalculateSqrt(int value){
			var num = 0;
			num = ((int)Math.Sqrt(value));

			return num;
		}

		public void SaveSqrt(IHeaderDictionary headers, int number, int result)
		{
			var key = "X-Evi-Tracking-Id";
			var trakingId = headers[key];

			const String OPERATION = "Sqrt";
			var journalController = new JournalController();
			var logController = new LogsController();
			var calculation = "";
			var date = DateTime.Now.ToString();
			var data = new string[3];

			if (trakingId != "xxx")
			{
				logController.saveInfor($"Find Traking-Id {trakingId}");
				calculation = "√" + number + " = " + result;
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
