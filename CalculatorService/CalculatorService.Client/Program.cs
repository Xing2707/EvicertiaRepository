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
			_clientLogs.Trace("Test Input");
			//While input is different than integer
			while (!int.TryParse(input, out var output))
			{
				_clientLogs.Error("Bad Input!");
				Console.WriteLine("Error! Introduce un numero correcto");
				input = Console.ReadLine();
			}
			_clientLogs.Trace("Input Correct");
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
			if (num < NUMBER) {
				option = Save();
			}

			//Create new object RestClient(url Localhost(http://localhost:5062/CalculatorService/))
			var client = new RestClient(host);
			_clientLogs.Trace("Create new RestClient");

			//Create new object RestRequest (url server(http://localhost:5062/CalculatorService/Add), Method = Post)
			var request = new RestRequest(path, Method.Post);
			_clientLogs.Trace("Create nee RestRequest");

			// Add request Header : Accept,Content-Type,Tracking-Id
			request.AddHeader("accept", "application/json");
			request.AddHeader("content-type", "application/json");
			_clientLogs.Trace("Add request header");

			if (option && num < NUMBER)
			{
				var id = CreateId();
				request.AddHeader("X-Evi-Tracking-Id", id);
				_clientLogs.Trace($"add Traking-Id {id} in request header with id created for save journal");
				Console.WriteLine("Se han generado id para puedes bucar historial \n tu id es: " + id);
			}
			else if (num < NUMBER)
			{
				request.AddHeader("X-Evi-Tracking-Id", "xxx");
				_clientLogs.Trace("add Traking-id xxx in request header for dont save journal");
			}

			// Add JsonBody
			request.AddJsonBody(data);
			_clientLogs.Trace("Add data format json in request body");

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

		//private static TResponse DoRequestFor<TRequest, TResponse>(TRequest model, string url)
		//{
		//	RestClient client = null; //< FIXME: Create here? the client.. with all the stuff.
		//	RestRequest request = null; //< FIXME: Build this here?
		//	// .. populate model somehow
		//	var response = client.Execute<TResponse>(request);
		//	if (response == null) { }
		//	else { }

		//	return response.Data; //< FIXME: Modify at your risk, whatever.
		//}

		private static void GetResponse<TRequest,TResponse>(RestClient client, RestRequest request,string name)
		{
			var response = client.Execute<TResponse>(request);
			if (response == null) {
				Console.WriteLine(response.ErrorMessage);
				_clientLogs.Error($"bad {name} response, print response error message.");
			}else{
				Console.WriteLine("resultado: " + response.Data);
				_clientLogs.Trace($"{name} response correct get {name} result and print");
			}
		}

		//Functions getresponse of each operations
		private static void GetAddResponse(RestClient client, RestRequest request)
		{
			//Create variable get response result execute
			var response = client.Execute<Addition.AddResponse>(request);
			_clientLogs.Trace("Create new addition response get server response");

			//If response is null show errror messager else show response result
			if (response == null)
			{
				Console.Write(response.ErrorMessage);
				_clientLogs.Error("bad addition response print response error message");
			}
			else
			{
				var addResponse = response.Data;
				Console.Write("resultado: " + addResponse.Sum);
				_clientLogs.Trace("Addition response correct get addition result and print");
			}
		}

		private static void GetSubResponse(RestClient client, RestRequest request)
		{
			var response = client.Execute<Subtraction.SubResponse>(request);
			_clientLogs.Trace("Create new subtraction response get server response");

			if (response == null)
			{
				Console.Write(response.ErrorMessage);
				_clientLogs.Error("bad subtraction response print response error message");
			}
			else
			{
				var subResponse = response.Data;
				Console.Write("resultado: " + subResponse.Difference);
				_clientLogs.Trace("Subtraction response correct get subtraction result and print");
			}
		}

		private static void GetMultResponse(RestClient client, RestRequest request)
		{
			var response = client.Execute<Multiplication.MultResponse>(request);
			_clientLogs.Trace("Create new multiplication response get server response");

			if (response == null)
			{
				Console.Write(response.ErrorMessage);
				_clientLogs.Error("bad multiplication response print response error message");
			}
			else
			{
				var multResponse = response.Data;
				Console.Write("resultado: " + multResponse.Product);
				_clientLogs.Trace("Multiplication response correct get multiplication result and print");
			}
		}
		private static void GetDivResponse(RestClient client, RestRequest request)
		{
			var response = client.Execute<Divide.DivResponse>(request);
			_clientLogs.Trace("Create new divide response get server response");

			if (response == null)
			{
				Console.Write(response.ErrorMessage);
				_clientLogs.Error("bad divide response print response error message");
			}
			else
			{
				var divResponse = response.Data;
				Console.Write("resultado: \nCociente: " + divResponse.Quotient + "\nResto: " + divResponse.Remainder);
				_clientLogs.Trace("Divide response correct get divide result and print");
			}
		}
		private static void GetSqrtResponse(RestClient client, RestRequest request)
		{
			var response = client.Execute<Square.SqrtResponse>(request);
			_clientLogs.Trace("Create new square response get server response");

			if (response == null)
			{
				Console.WriteLine(response.ErrorMessage);
				_clientLogs.Error("bad square response print response error message");
			}
			else
			{
				var sqrtResponse = response.Data;
				Console.Write("resultado: " + sqrtResponse.Square);
				_clientLogs.Trace("Square response correct get square result and print");
			}
		}

		private static void GetJournalResponse(RestClient client, RestRequest request)
		{
			var response = client.Execute<Journal.journalResponse>(request);
			_clientLogs.Trace("Create new journal response get server response");
			if (response == null)
			{
				Console.WriteLine(response.ErrorMessage);
				_clientLogs.Error("bad journal response print response error message");
			}
			else
			{
				var journalResponse = response.Data;
				Console.Write("Operacion: " + journalResponse.Operation + "\n Calculation: " + journalResponse.Calculation + "\n Date: " + journalResponse.Date);
				_clientLogs.Trace("Journal response correct get journal data and print");
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
			_clientLogs.Trace("Get input format to lowercase for save or no save journal");
			while(!result.Contains(SI) && !result.Contains(NO))
			{
				_clientLogs.Error("Bad input!");
				Console.WriteLine("Erro! No Puedes Escribir valor que no sea Si o No !");
				result = Console.ReadLine().ToLower();
			}
			if (result.Contains(SI))
			{
				_clientLogs.Trace("Save Journal");
				return option;
			}
			else
			{
				_clientLogs.Trace("Dont Save Journal");
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
			_clientLogs.Trace("Crate traking-id random with length 5");
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
		private static Logger _clientLogs = LogManager.GetCurrentClassLogger();

		#endregion

		public static void Main()
		{
			//Calculator Menu
			do
			{
				_clientLogs.Trace("Inicialize caluculato menu");
				Console.WriteLine("\nCALCULATOR\n 1.Suma\n 2.Diferencia\n 3.Multiplicacion\n 4.Division\n 5.Raiz\n 6.Consulta Historial con ID\n 7Salir");
				input = Console.ReadLine();
				_clientLogs.Trace("Get menu input");
				while (!int.TryParse(input, out var output) || int.Parse(input) > 7)
				{
					_clientLogs.Error("Bad input!");
					Console.WriteLine("Error! introduce un numero correcto!");
					input = Console.ReadLine();
				}
				_clientLogs.Trace("Input correct");
				num = int.Parse(input);
				switch (num)
				{
					case ((int)menu.Addition):
						_clientLogs.Trace("Inicialize calculate addition");
						//Create new Object Addition.AddRequest
						var sum = new Addition.AddRequest();

						//Initialize array object
						sum.Addends = new int[3];
						_clientLogs.Trace("Inicialize array[3]");

						Console.WriteLine("Introduce numero que quieres sumar");
						_clientLogs.Trace("Get addition input in array");

						//fill array object with input
						for (var i = 0; i < sum.Addends.Length; i++)
						{
							sum.Addends[i] = int.Parse(TestInput(Console.ReadLine()));
						}

						//call function SendeRequest and get Response
						SendRequest(num, HOST, urls.add, sum);

						break;
					case ((int)menu.Subtraction):

						_clientLogs.Trace("Inicialize calculate subtraction");
						var difference = new Subtraction.SubRequest();
						Console.WriteLine("Introduce Numero que quieres hace diferencia");
						_clientLogs.Trace("Get subtraction inputs");

						difference.Minuend = int.Parse(TestInput(Console.ReadLine()));
						difference.Subtrahend = int.Parse(TestInput(Console.ReadLine()));

						SendRequest(num, HOST, urls.sub, difference);

						break;
					case ((int)menu.Multiplication):

						_clientLogs.Trace("Inicialize calculate multiplication");
						var mult = new Multiplication.MultRequest();
						mult.Factors = new int[3];
						_clientLogs.Trace("Inicialize array[3]");
						Console.WriteLine("Introduce numero que quieres multiplicar");
						_clientLogs.Trace("Get multiplication input in array");

						for (int i = 0; i < mult.Factors.Length; i++)
						{
							mult.Factors[i] = int.Parse(TestInput(Console.ReadLine()));
						}

						SendRequest(num, HOST, urls.mult, mult);

						break;
					case ((int)menu.Divide):

						_clientLogs.Trace("Inicialize calculate divide");
						var div = new Divide.DivRequest();
						Console.WriteLine("Introduce Numero que quieres dividir");
						_clientLogs.Trace("Get divide inputs");

						div.Dividend = int.Parse(TestInput(Console.ReadLine()));
						div.Divisor = int.Parse(TestInput(Console.ReadLine()));

						while (div.Divisor == 0)
						{
							_clientLogs.Error("Error! divisor input is 0!");
							Console.WriteLine("Error! No Puedes Introducir Numero 0 como divisor!\n Introduzca otra vez numero de divisor!");
							_clientLogs.Trace("Get divisor input");
							div.Divisor = int.Parse(TestInput(Console.ReadLine()));
						};

						SendRequest(num, HOST, urls.div, div);

						break;
					case ((int)menu.Square):

						_clientLogs.Trace("Inicialize calculate square");
						var sqrt = new Square.SqrtRequest();
						Console.WriteLine("Introduce base de raiz");
						_clientLogs.Trace("Get square input");
						sqrt.Number = int.Parse(TestInput(Console.ReadLine()));

						SendRequest(num, HOST, urls.sqrt, sqrt);

						break;
					case ((int)menu.Journal):

						_clientLogs.Trace("Inicialize Journal");
						var query = new Journal.JournalRequest();
						Console.WriteLine("Introduce ID de historia");
						query.Id = Console.ReadLine().ToUpper();
						_clientLogs.Trace("Get Journal input and format input to uppercase");
						while (string.IsNullOrEmpty(query.Id) || query.Id.Length != 5)
						{
							_clientLogs.Error("Error! input is null or input length is different 5");
							Console.WriteLine("Error! No Puedes no introducir nada o id de un longitud diferente que 5");
							query.Id = Console.ReadLine().ToUpper();
						}

						SendRequest(num, HOST, urls.journal + query.Id, query);
						break;
				}
			} while (num != 7);
			_clientLogs.Trace("End Calculato");
		}
	}
}