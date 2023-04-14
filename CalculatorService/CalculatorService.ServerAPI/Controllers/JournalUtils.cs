using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Collections.Concurrent;

namespace CalculatorService.ServerAPI.Controllers
{
	public static class JournalUtils
	{
		private static	ConcurrentDictionary<string, string[]> journalDictionary = new ConcurrentDictionary<string, string[]>();
		private static Logger _serverLogger = LogManager.GetCurrentClassLogger();

		public static void SaveJournalData(string id, string[] value)
		{
			journalDictionary.TryAdd(id, value);
			_serverLogger.Info($"Save journal {id}");
		}

		public static string[] GetJournalData(string id)
		{
			_serverLogger.Info($"Get journal data {id}");
			return journalDictionary.GetValueOrDefault(id);
		}


	}
}
