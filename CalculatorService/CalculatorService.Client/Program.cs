using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text;
using System.Xml;
using CalculatorService.Library;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Nodes;
using RestSharp;
using CalculatorService.ServerAPI.Models;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Http.Headers;
using System.Drawing;

namespace CalculatorService.Client
{
	public class Program
	{
		private const string host = "http://localhost:5062/CalculatorService/";
		public enum menu
		{
			suma =1,
			diferencia = 2,
			multiplicacion = 3,
			division = 4,
			raiz = 5,
			Consulta = 6
		}

		public class urls{
			public const string add = host + "Add";
			public const string sub =host + "Sub";
			public const string mult = host + "Mult";
			public const string div = host + "Div";
			public const string sqrt = host + "Sqrt";
			public const string query = host + "Query";
		}
		public static void Main()
		{
			string input;
			int num;
			Console.WriteLine("CALCULATOR\n 1.Suma\n 2.Diferencia\n 3.Multiplicacion\n 4.Division\n 5.Raiz\n 6.Consulta Historial con ID");
			input = Console.ReadLine();
			while(!int.TryParse(input, out var output) || int.Parse(input) > 6 ){
				Console.WriteLine("Error! Introduce un numero correcto!");
				input= Console.ReadLine();
			}
			num = int.Parse(input);
			
			switch(num){
				case ((int)menu.suma):
					var sum = new AddRequest();
					sum.Addends = new int[3];
					Console.WriteLine("Introduce Numero que quieres sumar");
					for(var i = 0; i<sum.Addends.Length; i++)
					{
						sum.Addends[i] = int.Parse(TestInput(Console.ReadLine()));
					}

					SendRequest(num,host,urls.add, sum);

					break;
				case ((int)menu.diferencia):
					var difference = new SubRequest();
					Console.WriteLine("Introduce Numero que quieres hace diferencia");
					difference.Minuend = int.Parse(TestInput(Console.ReadLine()));
					difference.Subtrahend = int.Parse(TestInput(Console.ReadLine()));
					SendRequest(num,host,urls.sub, difference);

					break;
				case ((int)menu.multiplicacion):
					var mult = new MultRequest();
					mult.Factors = new int[3];	
					Console.WriteLine("Introduce Numero que quieres multiplicar");
					for (int i = 0; i < mult.Factors.Length; i++)
					{
						mult.Factors[i] = int.Parse(TestInput(Console.ReadLine()));
					}
					SendRequest(num, host, urls.mult, mult);

					break;
				case ((int)menu.division):
					var div = new DivRequest();
					Console.WriteLine("Introduce Numero que quieres dividir");
					div.Dividend = int.Parse(TestInput(Console.ReadLine()));
					div.Divisor = int.Parse(TestInput(Console.ReadLine()));
					while (div.Dividend < 0) {
						Console.WriteLine("Error! No Puedes Introducir Numero 0 como divisor!");
						div.Divisor = int.Parse(TestInput(Console.ReadLine()));
					};
					SendRequest(num, host, urls.div, div);

					break;
				case ((int)menu.raiz):
					var sqrt = new SqrtRequest();
					Console.WriteLine("Introduce base de raiz");
					sqrt.Number = int.Parse(TestInput(Console.ReadLine()));
					SendRequest(num, host, urls.sqrt, sqrt);

				break;
				case((int)menu.Consulta):
					string consulta;
					Console.WriteLine("Introduce ID de historia");
					consulta = Console.ReadLine();
					while(string.IsNullOrEmpty(consulta) || consulta.Length != 3)
					{
						Console.WriteLine("Error! No Puedes no introducir nada o id de un longitud diferente que 3");
						consulta = Console.ReadLine();
					}
				break;

			}

			string TestInput(string input)
			{
				while (!int.TryParse(input, out var output))
				{
					Console.WriteLine("Error! Introduce un numero correcto");
					input = Console.ReadLine();
				}
				return input;
			}

			async void SendRequest(int num, string host,string path, object data)
			{

				var client = new RestClient(host);
				var request = new RestRequest(path,Method.Post);
				var length = data.ToString().Length;
				request.AddHeader("Accept", "application/json");
				request.AddHeader("Content-Type", "application/json");
				//request.AddHeader<int>("Content-Length", length);
				request.AddHeader("X-Evi-Tracking-Id", "xxx");
				request.AddJsonBody(data);
				
				switch(num)
				{
					case ((int)menu.suma):
						GetAddResponse(client, request);
					break;
					case ((int)menu.diferencia):
						GetSubResponse(client,request);
					break;
					case ((int)menu.multiplicacion):
						GetMultResponse(client,request);
					break;
					case ((int)menu.division):
						GetDivResponse(client,request);
					break;
					case ((int)menu.raiz):
						GetSqrtResponse(client,request);
					break;

				}
			}

			async void GetAddResponse(RestClient client, RestRequest request)
			{
				var response = client.Execute<AddResponse>(request);
				var addResponse = response.Data;
				Console.Write("resultado: " + addResponse.Sum);
			}

			async void GetSubResponse(RestClient client, RestRequest request)
			{
				var response = client.Execute<SubResponse>(request);
				var subResponse = response.Data;
				Console.Write("resultado: " + subResponse.Difference);
			}
			
			async void GetMultResponse(RestClient client, RestRequest request)
			{
				var response = client.Execute<MultResponse>(request);
				var multResponse = response.Data;
				Console.Write("resultado: " + multResponse.Product);
			}
			async void GetDivResponse(RestClient client,RestRequest request)
			{
				var response = client.Execute<DivResponse>(request);
				var divResponse = response.Data;
				Console.Write("resultado: \nCociente: " + divResponse.Quotient +"\nResto: "+divResponse.Remainder);
			}
			async void GetSqrtResponse(RestClient client,RestRequest request){
				var response = client.Execute<SqrtResponse>(request);
				var sqrtResponse = response.Data;
				Console.Write("resultado: " +sqrtResponse.Square);
			}
		}
	}
}