using ApiConsumer.Models;
using System.Net.Http.Json;

namespace ApiConsumer.Services
{
	public class StudentService : IService<Student>
	{
		private readonly HttpClient httpClient;

		public StudentService(HttpClient _httpClient)
		{
			httpClient = _httpClient;
		}

		public async Task Edit(int id, Student entity)
		{
			await httpClient.PutAsJsonAsync<Student>($"api/Students/{id}", entity);
		}

		public async Task<List<Student>> GetAll()
		{
			List<Student> stds = await httpClient.GetFromJsonAsync<List<Student>>("api/Students");
			return stds;
		}
		public async Task<int> GetStudentsCount()
		{
			var stds = await httpClient.GetFromJsonAsync<List<Student>>("api/Students");
			return stds.Count();
		}

		public async Task<Student> GetById(int id)
		{
			var std = await httpClient.GetFromJsonAsync<Student>($"api/Students/{id}");
			return std;
		}

		public Task<int> GetDeptsCount()
		{
			throw new NotImplementedException();
		}
	}
}
