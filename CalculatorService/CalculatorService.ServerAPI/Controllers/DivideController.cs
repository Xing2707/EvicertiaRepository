using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace CalculatorService.ServerAPI.Controllers
{
	public class DivideController : Controller
	{
		public int[] CalculateDiv(int dividend, int divisor){
			var result = new int[2];
			result[0] = dividend/divisor;
			result[1] = dividend%divisor;
			return result;
		}

		public void SaveDiv(IHeaderDictionary headers, int dividend, int divisor, int quotient, int remainder)
		{
			var key = "X-Evi-Tracking-Id";
			var trakingId = headers[key];

			const String OPERATION = "Div";
			var journalController = new JournalController();
			var logController = new LogsController();
			var calculation ="";
			var date = DateTime.Now.ToString();
			var data = new string[3];

			if (trakingId != "xxx")
			{
				logController.saveInfor($"Find Traking-Id {trakingId}");
				calculation = dividend + " / " + divisor + " = " + quotient + "\n Restor: " + remainder;
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
