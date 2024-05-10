
using Day2.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Day2
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddDbContext<ITIContext>(options =>
			{
				options.UseLazyLoadingProxies().UseSqlServer("Data Source=.;Initial Catalog=ITI;Integrated Security=True;Encrypt=False");
			});
			builder.Services.AddSwaggerDocument();
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("CorsAllowed",
				builder =>
				{
					builder.AllowAnyOrigin();
					builder.AllowAnyMethod();
					builder.AllowAnyHeader();
				});
			});
			builder.Services.AddSwaggerGen(c =>
			{
				var filePath = Path.Combine(System.AppContext.BaseDirectory, "Day2.xml");
				c.IncludeXmlComments(filePath);
				c.EnableAnnotations();
				c.SwaggerDoc("v1",
				new OpenApiInfo
				{

					Title = "Api For Managing Student & Dept",
					Version = "v1",
					Description = "A sample API to demo Swashbuckle",
					TermsOfService = new Uri("http://tempuri.org/terms"),
					Contact = new OpenApiContact
					{
						Name = "Esraa Muhammed",
						Email = "esraa.mohamed.elmetwally@gmail.com"
					},
					
			}
				);
			});
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseOpenApi();
			app.UseSwaggerUI();
			app.UseCors("CorsAllowed");
			app.UseHttpsRedirection();

			app.UseAuthorization();
			app.MapControllers();

			app.Run();
		}
	}
}
