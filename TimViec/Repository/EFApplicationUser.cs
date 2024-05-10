using Microsoft.EntityFrameworkCore;
using TimViec.Data;
using TimViec.Models;
using TimViec.ViewModel;

namespace TimViec.Repository
{
	public class EFApplicationUser : IApplicationUser
	{
		private readonly ApplicationDbContext _context;
		public EFApplicationUser(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
		{
			return await _context.applicationUsers.ToListAsync();
		}

		public async Task<ApplicationUser> GetByIdAsync(int Id)
		{
			return await _context.applicationUsers.FindAsync(Id);
		}
		public async Task AddAsync(ApplicationUser applicationUser)
		{
			_context.applicationUsers.Add(applicationUser);
			await _context.SaveChangesAsync();
		}
		public async Task UpdateAsync(ApplicationUser applicationUser)
		{
			_context.applicationUsers.Update(applicationUser);
			await _context.SaveChangesAsync();
		}
		public async Task DeleteAsync(int id)
		{
			var user = await _context.applicationUsers.FindAsync(id);
			_context.applicationUsers.Remove(user);
			await _context.SaveChangesAsync();
		}

		public List<ViewAccountUserModel> GetAllUser(string role)
		{
			var result = from a in _context.applicationUsers
						 join ur in _context.UserRoles on a.Id equals ur.UserId
                         join r in _context.Roles on ur.RoleId equals r.Id
                         where (r.Name.Contains(role))
						 select new ViewAccountUserModel
						 {
							 fullname = a.Fullname,
							 email = a.Email,
							 Birth = a.Birth,
							 phonenumber = a.PhoneNumber,
						 };
			return result.ToList();
		}
	}
}
