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
			public string? Id { get; set; }
		}

		public class journalResponse
		{
			public string? Operation { get; set; }
			public string? Calculation { get; set; }
			public string?  Date { get; set; }

			public static journalResponse Result(string operation, string calculation, string date)
			{
				return new journalResponse { Operation = operation, Calculation = calculation, Date = date };
			}

			public static journalResponse NoIdSelect()
			{
				const string OPERATION = "null";
				const string CALCULATION = "No existe tal ID que buscas";
				string date = DateTime.Now.ToString();
				return new journalResponse { Operation = OPERATION, Calculation = CALCULATION, Date = date };
			}
		}
	}
}
