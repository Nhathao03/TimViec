using TimViec.Models;
using TimViec.ViewModel;

namespace TimViec.Respository
{
	public interface IJobRespository
	{
		Task<IEnumerable<Job>> GetAllAsync();
		Task<Job> GetByIdAsync(int id);
		Task AddAsync(Job job);
		Task UpdateAsync(Job job);
		Task DeleteAsync(int id);

		List<SearchViewModel> Search(string stringSearch);
		List<Details_CPN> Details_CPN(int ID);

	}
}
