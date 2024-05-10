using TimViec.Data;
using TimViec.Models;
using Microsoft.EntityFrameworkCore;
using TimViec.ViewModel;

namespace TimViec.Respository
{
    public class EFCompanyRespository : ICompanyRespository
    {
        private readonly ApplicationDbContext _context;
        public EFCompanyRespository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _context.Companies.ToListAsync();
        }
        public async Task<Company> GetByIdAsync(int Id)
        {
            return await _context.Companies.FindAsync(Id);
        }
        public async Task AddAsync(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Company company)
        {
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
        }

		public List<Details_CPN> Details_CPN(int ID)
		{
			var result = from j in _context.Jobs
						 join c in _context.Companies on j.CompanyID equals c.Id
                         join t in _context.Type_Works on j.Type_workID equals t.Id
						 where (c.Id.Equals(ID))
						 select new Details_CPN
						 {
							 CompanyName1 = c.Name_company,
							 JobName = j.Title,
							 Email = c.Email,
							 Company_Size = c.Company_size,
							 Company_Type = t.Type,
							 R1_Language = j.R1_Language,
							 R2_Language = j.R2_Language,
							 R3_Language = j.R3_Language,
							 Description = c.Description,
							 DescriptionJob = j.Description,
							 Location = c.Location,
							 LocationJob = j.Location,
							 Image = c.Image,
							 ImageJob = j.img,
							 IDJob = j.Id

						 };
			return result.ToList();
		}
	}
}
