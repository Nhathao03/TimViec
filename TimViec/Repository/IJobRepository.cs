using TimViec.Models;

namespace TimViec.Respository
{
	public interface IJobRespository
	{
		Task<IEnumerable<Job>> GetAllAsync();
		Task<Job> GetByIdAsync(int id);
		Task AddAsync(Job job);
		Task UpdateAsync(Job job);
		Task DeleteAsync(int id);
	}
}
