using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorService.Library
{
	public class Journal
	{
		public class JournalRequet
		{
			public string Id { get; set; }
		}

		public class journalResponse
		{
			public string Operartion { get; set; }
			public string Calculation { get; set; }
			public string  Date { get; set; }

			public static journalResponse Result(string operation, string calculation, string date)
			{
				return new journalResponse { Operartion = operation, Calculation = calculation, Date = date };
			}
		}
	}
}
