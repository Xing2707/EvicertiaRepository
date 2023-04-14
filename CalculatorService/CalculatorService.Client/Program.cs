using CalculatorService.Library;
using NLog;
using RestSharp;

namespace CalculatorService.Client
{
	public class Program
	{
		#region TestIput Function

		//This function validates the input, while ipunt is not parsing to the integer, it displays an error message and asks the user to enter another value until the entered value can be passed to an integer and return the value
		private static string TestInput(string input)
		{
			loggs.saveInfor("Test Input");
			//While input is different than integer
			while (!int.TryParse(input, out var output))
			{
				loggs.saveInfor("Bad Input");
				Console.WriteLine("Error! Introduce un numero correcto");
				input = Console.ReadLine();
			}
			return input;
		}
		#endregion

		#region SendRequestAndGetResponse Function

		//Send request to served and call GetResponse function to get result
		//paramete (menu num select, url localhost, url server complet, object json)
		private static void SendRequest(int num, string host, string path, object data)
		{
			bool option = false;
			if( num < 6){
				option = Save();
			}

			//Create new object RestClient(url Localhost(http://localhost:5062/CalculatorService/))
			var client = new RestClient(host);

			//Create new object RestRequest (url server(http://localhost:5062/CalculatorService/Add), Method = Post)
			var request = new RestRequest(path, Method.Post);

			// Add request Header : Accept,Content-Type,Tracking-Id
			request.AddHeader("accept", "application/json");
			request.AddHeader("content-type", "application/json");

			if (option && num < 6)
			{
				var id = CreateId();
				request.AddHeader("X-Evi-Tracking-Id", id);
				Console.WriteLine("Se han generado id para puedes bucar historial \n tu id es: "+id);
			}
			else if(num < 6)
			{
				request.AddHeader("X-Evi-Tracking-Id", "xxx");
			}

			// Add JsonBody
			request.AddJsonBody(data);

			//Menu call response function of each operation
			switch (num)
			{
				case ((int)menu.suma):
					GetAddResponse(client, request);
					break;
				case ((int)menu.diferencia):
					GetSubResponse(client, request);
					break;
				case ((int)menu.multiplicacion):
					GetMultResponse(client, request);
					break;
				case ((int)menu.division):
					GetDivResponse(client, request);
					break;
				case ((int)menu.raiz):
					GetSqrtResponse(client, request);
					break;
				case ((int)menu.Consulta):
					GetJournalResponse(client, request);
					break;
			}
		}

		//Functions getresponse of each operations
		private static void GetAddResponse(RestClient client, RestRequest request)
		{
			//Create variable get response result execute
			var response = client.Execute<Addition.AddResponse>(request);

			//If response is null show errror messager else show response result
			if (response == null)
			{
				Console.Write(response.ErrorMessage);
			}
			else
			{
				var addResponse = response.Data;
				Console.Write("resultado: " + addResponse.Sum);
			}
		}

		private static void GetSubResponse(RestClient client, RestRequest request)
		{
			var response = client.Execute<Subtraction.SubResponse>(request);

			if (response == null)
			{
				Console.Write(response.ErrorMessage);
			}
			else
			{
				var subResponse = response.Data;
				Console.Write("resultado: " + subResponse.Difference);
			}
		}

		private static void GetMultResponse(RestClient client, RestRequest request)
		{
			var response = client.Execute<Multiplication.MultResponse>(request);

			if (response == null)
			{
				Console.Write(response.ErrorMessage);
			}
			else
			{
				var multResponse = response.Data;
				Console.Write("resultado: " + multResponse.Product);
			}
		}
		private static void GetDivResponse(RestClient client, RestRequest request)
		{
			var response = client.Execute<Divide.DivResponse>(request);

			if (response == null)
			{
				Console.Write(response.ErrorMessage);
			}
			else
			{
				var divResponse = response.Data;
				Console.Write("resultado: \nCociente: " + divResponse.Quotient + "\nResto: " + divResponse.Remainder);
			}
		}
		private static void GetSqrtResponse(RestClient client, RestRequest request)
		{
			var response = client.Execute<Square.SqrtResponse>(request);
			if (response == null)
			{
				Console.WriteLine(response.ErrorMessage);
			}
			else
			{
				var sqrtResponse = response.Data;
				Console.Write("resultado: " + sqrtResponse.Square);
			}
		}

		private static void GetJournalResponse(RestClient client, RestRequest request)
		{
			var response = client.Execute<Journal.journalResponse>(request);
			if(response == null)
			{
				Console.WriteLine(response.ErrorMessage);
			}
			else
			{
				var journalResponse = response.Data;
				Console.Write("Operacion: " + journalResponse.Operation + "\n Calculation: " + journalResponse.Calculation + "\n Date: " + journalResponse.Date);
			}
		}
		#endregion

		#region Save Function
		private static bool Save()
		{
			string result;
			var option = true;
			const string SI = "s";
			const string NO = "n";

			Console.WriteLine("Quires guardar tu operacion?");
			result = Console.ReadLine().ToLower();
			while(!result.Contains(SI) && !result.Contains(NO))
			{
				Console.WriteLine("Erro! No Puedes Escribir valor que no sea Si o No !");
				result = Console.ReadLine().ToLower();
			}
			if (result.Contains(SI))
			{
				return option;
			}
			else
			{
				option = false;
				return false;
			}
		}
		#endregion

		#region CreateId Function
		private static string CreateId()
		{
			const int LENGTH = 5;
			var id = "";
			var random = new Random();
			for(var i = 0; i < LENGTH; i++)
			{
				id += Convert.ToChar(random.Next(65, 90));
			}

			return id;
		}
		#endregion

		#region EnumAndConstVariable
		private enum menu
		{
			suma =1,
			diferencia = 2,
			multiplicacion = 3,
			division = 4,
			raiz = 5,
			Consulta = 6
		}

		private static class urls{
			public const string add = HOST + "Add";
			public const string sub = HOST + "Sub";
			public const string mult = HOST + "Mult";
			public const string div = HOST + "Div";
			public const string sqrt = HOST + "Sqrt";
			public const string journal = HOST + "Journal/";
		}

		private const string HOST = "http://localhost:5062/CalculatorService/";
		private static int num;
		private static string input;
		private static Logs loggs = new Logs();

		#endregion

		public static void Main()
		{
			//Calculator Menu
			do
			{
				Console.WriteLine("\nCALCULATOR\n 1.Suma\n 2.Diferencia\n 3.Multiplicacion\n 4.Division\n 5.Raiz\n 6.Consulta Historial con ID\n 7Salir");
				input = Console.ReadLine();
				while (!int.TryParse(input, out var output) || int.Parse(input) > 7)
				{
					loggs.saveInfor("Bad Input!");
					Console.WriteLine("Error! Introduce un numero correcto!");
					input = Console.ReadLine();
				}
				num = int.Parse(input);
				switch (num)
				{
					case ((int)menu.suma):
						//Create new Object Addition.AddRequest
						var sum = new Addition.AddRequest();

						//Initialize array object
						sum.Addends = new int[3];
						loggs.saveInfor("Inicialize Array[3]");

						Console.WriteLine("Introduce Numero que quieres sumar");

						//fill array object with input
						for (var i = 0; i < sum.Addends.Length; i++)
						{
							loggs.saveInfor("Set input  in array");
							sum.Addends[i] = int.Parse(TestInput(Console.ReadLine()));
						}

						//call function SendeRequest and get Response
						SendRequest(num, HOST, urls.add, sum);

						break;
					case ((int)menu.diferencia):
						var difference = new Subtraction.SubRequest();
						Console.WriteLine("Introduce Numero que quieres hace diferencia");

						difference.Minuend = int.Parse(TestInput(Console.ReadLine()));
						difference.Subtrahend = int.Parse(TestInput(Console.ReadLine()));

						SendRequest(num, HOST, urls.sub, difference);

						break;
					case ((int)menu.multiplicacion):
						var mult = new Multiplication.MultRequest();
						mult.Factors = new int[3];

						Console.WriteLine("Introduce Numero que quieres multiplicar");

						for (int i = 0; i < mult.Factors.Length; i++)
						{
							mult.Factors[i] = int.Parse(TestInput(Console.ReadLine()));
						}

						SendRequest(num, HOST, urls.mult, mult);

						break;
					case ((int)menu.division):

						var div = new Divide.DivRequest();
						Console.WriteLine("Introduce Numero que quieres dividir");

						div.Dividend = int.Parse(TestInput(Console.ReadLine()));
						div.Divisor = int.Parse(TestInput(Console.ReadLine()));

						while (div.Divisor == 0)
						{
							Console.WriteLine("Error! No Puedes Introducir Numero 0 como divisor!\n Introduzca otra vez numero de divisor!");
							div.Divisor = int.Parse(TestInput(Console.ReadLine()));
						};

						SendRequest(num, HOST, urls.div, div);

						break;
					case ((int)menu.raiz):

						var sqrt = new Square.SqrtRequest();
						Console.WriteLine("Introduce base de raiz");

						sqrt.Number = int.Parse(TestInput(Console.ReadLine()));

						SendRequest(num, HOST, urls.sqrt, sqrt);

						break;
					case ((int)menu.Consulta):

						var query = new Journal.JournalRequet();
						Console.WriteLine("Introduce ID de historia");
						query.Id = Console.ReadLine().ToUpper();
						while (string.IsNullOrEmpty(query.Id) || query.Id.Length != 5)
						{
							Console.WriteLine("Error! No Puedes no introducir nada o id de un longitud diferente que 5");
							query.Id = Console.ReadLine().ToUpper();
						}

						SendRequest(num, HOST, urls.journal + query.Id, query);
						break;
				}
			} while (num != 7);
		}
	}
}