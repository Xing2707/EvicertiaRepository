using CalculatorService.Library;
using NLog;
using RestSharp;
using System.Globalization;

namespace CalculatorService.Client
{
	public class Program
	{
		#region TestIput Function

		//This function validates the input, while ipunt is not parsing to the integer, it displays an error message and asks the user to enter another value until the entered value can be passed to an integer and return the value
		private static string TestInput(string input)
		{
			loggs.saveTrace("Test Input");
			//While input is different than integer
			while (!int.TryParse(input, out var output))
			{
				loggs.saveError("Bad Input!");
				Console.WriteLine("Error! Introduce un numero correcto");
				input = Console.ReadLine();
			}
			loggs.saveTrace("Input Correct");
			return input;
		}
		#endregion

		#region SendRequestAndGetResponse Function

		//Send request to served and call GetResponse function to get result
		//paramete (menu num select, url localhost, url server complet, object json)
		private static void SendRequest(int num, string host, string path, object data)
		{
			const int NUMBER = 6;
			bool option = false;
			if( num < NUMBER){
				option = Save();
			}

			//Create new object RestClient(url Localhost(http://localhost:5062/CalculatorService/))
			var client = new RestClient(host);
			loggs.saveTrace("Create new RestClient");

			//Create new object RestRequest (url server(http://localhost:5062/CalculatorService/Add), Method = Post)
			var request = new RestRequest(path, Method.Post);
			loggs.saveTrace("Create nee RestRequest");

			// Add request Header : Accept,Content-Type,Tracking-Id
			request.AddHeader("accept", "application/json");
			request.AddHeader("content-type", "application/json");
			loggs.saveTrace("Add request header");

			if (option && num < NUMBER)
			{
				var id = CreateId();
				request.AddHeader("X-Evi-Tracking-Id", id);
				loggs.saveTrace($"add Traking-Id {id} in request header with id created for save journal");
				Console.WriteLine("Se han generado id para puedes bucar historial \n tu id es: "+id);
			}
			else if(num < NUMBER)
			{
				request.AddHeader("X-Evi-Tracking-Id", "xxx");
				loggs.saveTrace("add Traking-id xxx in request header for dont save journal");
			}

			// Add JsonBody
			request.AddJsonBody(data);
			loggs.saveTrace("Add data format json in request body");

			//Menu call response function of each operation
			switch (num)
			{
				case ((int)menu.Addition):
					GetAddResponse(client, request);
					break;
				case ((int)menu.Subtraction):
					GetSubResponse(client, request);
					break;
				case ((int)menu.Multiplication):
					GetMultResponse(client, request);
					break;
				case ((int)menu.Divide):
					GetDivResponse(client, request);
					break;
				case ((int)menu.Square):
					GetSqrtResponse(client, request);
					break;
				case ((int)menu.Journal):
					GetJournalResponse(client, request);
					break;
			}
		}

		//Functions getresponse of each operations
		private static void GetAddResponse(RestClient client, RestRequest request)
		{
			//Create variable get response result execute
			var response = client.Execute<Addition.AddResponse>(request);
			loggs.saveTrace("Create new addition response get server response");

			//If response is null show errror messager else show response result
			if (response == null)
			{
				Console.Write(response.ErrorMessage);
				loggs.saveError("bad addition response print response error message");
			}
			else
			{
				var addResponse = response.Data;
				Console.Write("resultado: " + addResponse.Sum);
				loggs.saveTrace("Addition response correct get addition result and print");
			}
		}

		private static void GetSubResponse(RestClient client, RestRequest request)
		{
			var response = client.Execute<Subtraction.SubResponse>(request);
			loggs.saveTrace("Create new subtraction response get server response");

			if (response == null)
			{
				Console.Write(response.ErrorMessage);
				loggs.saveError("bad subtraction response print response error message");
			}
			else
			{
				var subResponse = response.Data;
				Console.Write("resultado: " + subResponse.Difference);
				loggs.saveTrace("Subtraction response correct get subtraction result and print");
			}
		}

		private static void GetMultResponse(RestClient client, RestRequest request)
		{
			var response = client.Execute<Multiplication.MultResponse>(request);
			loggs.saveTrace("Create new multiplication response get server response");

			if (response == null)
			{
				Console.Write(response.ErrorMessage);
				loggs.saveError("bad multiplication response print response error message");
			}
			else
			{
				var multResponse = response.Data;
				Console.Write("resultado: " + multResponse.Product);
				loggs.saveTrace("Multiplication response correct get multiplication result and print");
			}
		}
		private static void GetDivResponse(RestClient client, RestRequest request)
		{
			var response = client.Execute<Divide.DivResponse>(request);
			loggs.saveTrace("Create new divide response get server response");

			if (response == null)
			{
				Console.Write(response.ErrorMessage);
				loggs.saveError("bad divide response print response error message");
			}
			else
			{
				var divResponse = response.Data;
				Console.Write("resultado: \nCociente: " + divResponse.Quotient + "\nResto: " + divResponse.Remainder);
				loggs.saveTrace("Divide response correct get divide result and print");
			}
		}
		private static void GetSqrtResponse(RestClient client, RestRequest request)
		{
			var response = client.Execute<Square.SqrtResponse>(request);
			loggs.saveTrace("Create new square response get server response");

			if (response == null)
			{
				Console.WriteLine(response.ErrorMessage);
				loggs.saveError("bad square response print response error message");
			}
			else
			{
				var sqrtResponse = response.Data;
				Console.Write("resultado: " + sqrtResponse.Square);
				loggs.saveTrace("Square response correct get square result and print");
			}
		}

		private static void GetJournalResponse(RestClient client, RestRequest request)
		{
			var response = client.Execute<Journal.journalResponse>(request);
			loggs.saveTrace("Create new journal response get server response");
			if (response == null)
			{
				Console.WriteLine(response.ErrorMessage);
				loggs.saveError("bad journal response print response error message");
			}
			else
			{
				var journalResponse = response.Data;
				Console.Write("Operacion: " + journalResponse.Operation + "\n Calculation: " + journalResponse.Calculation + "\n Date: " + journalResponse.Date);
				loggs.saveTrace("Journal response correct get journal data and print");
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
			loggs.saveTrace("Get input format to lowercase for save or no save journal");
			while(!result.Contains(SI) && !result.Contains(NO))
			{
				loggs.saveError("Bad input!");
				Console.WriteLine("Erro! No Puedes Escribir valor que no sea Si o No !");
				result = Console.ReadLine().ToLower();
			}
			if (result.Contains(SI))
			{
				loggs.saveTrace("Save Journal");
				return option;
			}
			else
			{
				loggs.saveTrace("Dont Save Journal");
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
			loggs.saveTrace("Crate traking-id random with length 5");
			return id;
		}
		#endregion

		#region EnumAndConstVariable
		private enum menu
		{
			Addition =1,
			Subtraction = 2,
			Multiplication = 3,
			Divide = 4,
			Square = 5,
			Journal = 6
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
				loggs.saveTrace("Inicialize caluculato menu");
				Console.WriteLine("\nCALCULATOR\n 1.Suma\n 2.Diferencia\n 3.Multiplicacion\n 4.Division\n 5.Raiz\n 6.Consulta Historial con ID\n 7Salir");
				input = Console.ReadLine();
				loggs.saveTrace("Get menu input");
				while (!int.TryParse(input, out var output) || int.Parse(input) > 7)
				{
					loggs.saveError("Bad input!");
					Console.WriteLine("Error! introduce un numero correcto!");
					input = Console.ReadLine();
				}
				loggs.saveTrace("Input correct");
				num = int.Parse(input);
				switch (num)
				{
					case ((int)menu.Addition):
						loggs.saveTrace("Inicialize calculate addition");
						//Create new Object Addition.AddRequest
						var sum = new Addition.AddRequest();

						//Initialize array object
						sum.Addends = new int[3];
						loggs.saveTrace("Inicialize array[3]");

						Console.WriteLine("Introduce numero que quieres sumar");
						loggs.saveTrace("Get addition input in array");

						//fill array object with input
						for (var i = 0; i < sum.Addends.Length; i++)
						{
							sum.Addends[i] = int.Parse(TestInput(Console.ReadLine()));
						}

						//call function SendeRequest and get Response
						SendRequest(num, HOST, urls.add, sum);

						break;
					case ((int)menu.Subtraction):

						loggs.saveTrace("Inicialize calculate subtraction");
						var difference = new Subtraction.SubRequest();
						Console.WriteLine("Introduce Numero que quieres hace diferencia");
						loggs.saveTrace("Get subtraction inputs");

						difference.Minuend = int.Parse(TestInput(Console.ReadLine()));
						difference.Subtrahend = int.Parse(TestInput(Console.ReadLine()));

						SendRequest(num, HOST, urls.sub, difference);

						break;
					case ((int)menu.Multiplication):

						loggs.saveTrace("Inicialize calculate multiplication");
						var mult = new Multiplication.MultRequest();
						mult.Factors = new int[3];
						loggs.saveTrace("Inicialize array[3]");
						Console.WriteLine("Introduce numero que quieres multiplicar");
						loggs.saveTrace("Get multiplication input in array");

						for (int i = 0; i < mult.Factors.Length; i++)
						{
							mult.Factors[i] = int.Parse(TestInput(Console.ReadLine()));
						}

						SendRequest(num, HOST, urls.mult, mult);

						break;
					case ((int)menu.Divide):

						loggs.saveTrace("Inicialize calculate divide");
						var div = new Divide.DivRequest();
						Console.WriteLine("Introduce Numero que quieres dividir");
						loggs.saveTrace("Get divide inputs");

						div.Dividend = int.Parse(TestInput(Console.ReadLine()));
						div.Divisor = int.Parse(TestInput(Console.ReadLine()));

						while (div.Divisor == 0)
						{
							loggs.saveError("Error! divisor input is 0!");
							Console.WriteLine("Error! No Puedes Introducir Numero 0 como divisor!\n Introduzca otra vez numero de divisor!");
							loggs.saveTrace("Get divisor input");
							div.Divisor = int.Parse(TestInput(Console.ReadLine()));
						};

						SendRequest(num, HOST, urls.div, div);

						break;
					case ((int)menu.Square):

						loggs.saveTrace("Inicialize calculate square");
						var sqrt = new Square.SqrtRequest();
						Console.WriteLine("Introduce base de raiz");
						loggs.saveTrace("Get square input");
						sqrt.Number = int.Parse(TestInput(Console.ReadLine()));

						SendRequest(num, HOST, urls.sqrt, sqrt);

						break;
					case ((int)menu.Journal):

						loggs.saveTrace("Inicialize Journal");
						var query = new Journal.JournalRequet();
						Console.WriteLine("Introduce ID de historia");
						query.Id = Console.ReadLine().ToUpper();
						loggs.saveTrace("Get Journal input and format input to uppercase");
						while (string.IsNullOrEmpty(query.Id) || query.Id.Length != 5)
						{
							loggs.saveError("Error! input is null or input length is different 5");
							Console.WriteLine("Error! No Puedes no introducir nada o id de un longitud diferente que 5");
							query.Id = Console.ReadLine().ToUpper();
						}

						SendRequest(num, HOST, urls.journal + query.Id, query);
						break;
				}
			} while (num != 7);
			loggs.saveTrace("End Calculato");
		}
	}
}