using ApiConsumer.Models;

namespace ApiConsumer.Services
{
	public interface IService<T>
	{
		public Task<T> GetById(int id);
		public  Task Edit(int id, T entity);
		public  Task<List<T>> GetAll();
		public Task<int> GetStudentsCount();
		public Task<int> GetDeptsCount();
	}
}
