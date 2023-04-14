using NLog;

namespace CalculatorService.Client
{
	public class Logs
	{
		private static Logger clienteLogger = LogManager.GetCurrentClassLogger();

		public void saveTrace(string trace)
		{
			clienteLogger.Trace("\n     " + trace + "\n");
		}
		public void saveError(string error)
		{
			clienteLogger.Error("\n     " + error + "\n");
		}
	}
}
