using TimViec.Data;
using TimViec.Models;
using Microsoft.EntityFrameworkCore;

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
            return await _context.Jobs.Include(p =>
       p.Company).ToListAsync();
        }
        public async Task<Job> GetByIdAsync(int id)
        {
            return await _context.Jobs.Include(p =>
                      p.Company).FirstOrDefaultAsync(p => p.Id == id);
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
    }
}
