using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace CalculatorService.ServerAPI.Controllers
{
	public class JournalController : Controller
	{
		private static	ConcurrentDictionary<string, string[]> journalDictionary = new ConcurrentDictionary<string, string[]>();

		public void SaveJournalData(string id, string[] value)
		{
			journalDictionary.TryAdd(id, value);
		}

		public string[] GetJournalData(string id)
		{
			return journalDictionary.GetValueOrDefault(id);
		}


	}
}
