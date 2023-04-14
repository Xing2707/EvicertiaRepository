using NLog;
using NLog.Fluent;
using System.Security.AccessControl;

namespace CalculatorService.ServerAPI.Controllers
{
	public class LogsController
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();
		public void saveInfor(string infor)
		{
			logger.Info("\n     " + infor + "\n");
		}

		public void saveErrorLog(string error)
		{
			logger.Error("\n     " + error + "\n");
		}
	}
}
