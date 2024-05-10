using ApiConsumer.Models;
using System.Net.Http.Json;

namespace ApiConsumer.Services
{
	public class DeptService : IService<Dept>
	{
		public HttpClient httpClient;
		public DeptService(HttpClient _httpClient)
		{
			httpClient = _httpClient;
		}
		public async Task Edit(int id, Dept entity)
		{
			await httpClient.PutAsJsonAsync<Dept>($"api/Department/{id}", entity);
		}

		public async Task<List<Dept>> GetAll()
		{
			var Emplist =
				await httpClient.GetFromJsonAsync<List<Dept>>("api/Department");
			return Emplist;
		}
		public async Task<int> GetDeptsCount()
		{
			var depts = await httpClient.GetFromJsonAsync<List<Dept>>("api/Department");
			return depts.Count();
		}
		public async Task<Dept> GetById(int id)
		{
			Dept emp = await httpClient.GetFromJsonAsync<Dept>($"api/Department/{id}");
			return emp;
		}

		public Task<int> GetStudentsCount()
		{
			throw new NotImplementedException();
		}
	}
}
