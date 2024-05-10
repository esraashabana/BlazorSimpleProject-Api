using ApiConsumer.Models;
using ApiConsumer.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace ApiConsumer
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");
			builder.RootComponents.Add<HeadOutlet>("head::after");
			builder.Services.AddHttpClient<IService<Student>, StudentService>
			   (httpclient =>
				   httpclient.BaseAddress =
					   new Uri(builder.Configuration.GetValue<string>("Api")));
			builder.Services.AddHttpClient<IService<Dept>, DeptService>
			   (httpclient =>
				   httpclient.BaseAddress =
					   new Uri(builder.Configuration.GetValue<string>("Api")));
			await builder.Build().RunAsync();
		}
	}
}
