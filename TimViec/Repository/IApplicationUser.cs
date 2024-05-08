using TimViec.Models;
using TimViec.ViewModel;

namespace TimViec.Repository
{
	public interface IApplicationUser
	{
		Task<IEnumerable<ApplicationUser>> GetAllAsync();
		Task<ApplicationUser> GetByIdAsync(string id);
		Task<ApplicationUser> GetByIdAsyncUser(string id);
		Task AddAsync(ApplicationUser applicationUser);
		Task UpdateAsync(ApplicationUser applicationUser);
		Task DeleteAsync(int id);
		List<ViewAccountUserModel> GetAllUser(string role);

	}
}
