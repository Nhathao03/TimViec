using TimViec.Models;

namespace TimViec.Repository
{
	public interface IApplicationUser
	{
		Task<IEnumerable<ApplicationUser>> GetAllAsync();
		Task<ApplicationUser> GetByIdAsync(string id);
		Task<ApplicationUser> GetByIdAsyncUser(int id);
		Task AddAsync(ApplicationUser applicationUser);
		Task UpdateAsync(ApplicationUser applicationUser);
		Task DeleteAsync(int id);
		
	}
}
