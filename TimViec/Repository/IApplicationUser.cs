using TimViec.Models;
using TimViec.ViewModel;

namespace TimViec.Repository
{
	public interface IApplicationUser
	{
		Task<IEnumerable<ApplicationUser>> GetAllAsync();
		Task<ApplicationUser> GetByIdAsync(int id);
		Task AddAsync(ApplicationUser applicationUser);
		Task UpdateAsync(ApplicationUser applicationUser);
		Task DeleteAsync(int id);
		List<ViewAccountUserModel> GetAllUser(string role);
        List<ViewInforCompany> GetInforCompany(string email);

    }
}
