using NLog;

namespace CalculatorService.Client
{
	public class Logs
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		public void saveInfor(string infor)
		{
			logger.Info("\n     " + infor + "\n");
		}
		public void saveError(string error)
		{
			logger.Error(error);
		}
	}
}
