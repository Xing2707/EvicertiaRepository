using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace CalculatorService.ServerAPI.Controllers
{
	public class JournalController : Controller
	{
		private static	ConcurrentDictionary<string, string[]> journalDictionary = new ConcurrentDictionary<string, string[]>();
		private static LogsController logsController = new LogsController();

		public void SaveJournalData(string id, string[] value)
		{
			journalDictionary.TryAdd(id, value);
			logsController.saveInfor($"Save journal {id}");
		}

		public string[] GetJournalData(string id)
		{
			logsController.saveInfor($"Get journal data {id}");
			return journalDictionary.GetValueOrDefault(id);
		}


	}
}
