using CalculatorService.ServerAPI.Filters;
using CalculatorService.ServerAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorService.ServerAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Logging.ClearProviders();
			builder.Logging.AddConsole();

			builder.Services.AddRazorPages();

			// Add services to the container.
			builder.Services.AddControllers();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			//Remove BadRequest Filter
			builder.Services.Configure<ApiBehaviorOptions>(options =>
			{
				options.SuppressModelStateInvalidFilter = true;
			});

			//Add My BadRequestAttritute in swagger default BadRequest
			builder.Services.AddControllers(option => {
				option.Filters.Add(typeof(CustomBadRequestFilterAttribute));
				option.Filters.Add(typeof(CustomInternaErrorFilterAttribute));
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}