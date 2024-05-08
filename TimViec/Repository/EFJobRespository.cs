using TimViec.Data;
using TimViec.Models;
using Microsoft.EntityFrameworkCore;
using TimViec.ViewModel;

namespace TimViec.Respository
{
    public class EFJobRespository : IJobRespository
    {
        private readonly ApplicationDbContext _context;
        public EFJobRespository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Job>> GetAllAsync()
        {
            return await _context.Jobs.ToListAsync();
        }
        public async Task<Job> GetByIdAsync(int id)
        {
            return await _context.Jobs.FindAsync(id);
        }
        public async Task AddAsync(Job job)
        {
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Job job)
        {
            _context.Jobs.Update(job);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
        }

		public List<SearchViewModel> Search(string stringSearch)
		{
            var result = from j in _context.Jobs
                         join c in _context.Companies on j.CompanyID equals c.Id
                         where (j.Title.Contains(stringSearch)
                                || j.R1_Language.Contains(stringSearch)
                                || j.R2_Language.Contains(stringSearch)
                                || j.R3_Language.Contains(stringSearch)
                                || c.Name_company.Contains(stringSearch)
                                || c.Location.Contains(stringSearch))
                         select new SearchViewModel
                         {
                             Id = j.Id,
                             JobName = j.Title,
                             CompanyName = c.Name_company,
                             R1_Language = j.R1_Language,
                             R2_Language = j.R2_Language,
                             R3_Language = j.R3_Language,
                             Location = c.Location,
                             Salary = j.Salary,
                             Image = j.img,
                         };
		    return result.ToList();
        }

        public List<Details_CPN> Details_CPN(int ID)
		{
            var result = from j in _context.Jobs
                         join c in _context.Companies on j.CompanyID equals c.Id
                         where (c.Id.Equals(ID))
                         select new Details_CPN
                         {
                             CompanyName1 = c.Name_company,
                             JobName = j.Title,
                             Email = c.Email,
                             Company_Size = c.Company_size,
                             Company_Type = c.Company_type,
                             R1_Language = j.R1_Language,
                             R2_Language = j.R2_Language,
                             R3_Language = j.R3_Language,
                             Description = c.Description,
                             DescriptionJob = j.Description,
                             Location = c.Location,
                             LocationJob = j.Location,
                             Image = c.Image,
                             ImageJob = j.img

                         };


		    return result.ToList();
        }

    }
}
