using NLog;

namespace CalculatorService.ServerAPI.Controllers
{
	public class LogsController
	{
		private static Logger serverLogger = LogManager.GetCurrentClassLogger();
		public void saveInfor(string infor)
		{
			serverLogger.Info("\n     " + infor + "\n");
			serverLogger.Trace("\n     " + infor + "\n");
		}

		public void saveErrorLog(string error)
		{
			serverLogger.Error("\n     " + error + "\n");
		}
	}
}
