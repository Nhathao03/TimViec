using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.Security.Cryptography.Xml;
using TimViec.Data;
using TimViec.Models;

namespace TimViec.Repository
{
    public class EFStatusJobRepository :IStatusRepository
    {
        private readonly ApplicationDbContext _context;
        public EFStatusJobRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<StatusJob>> GetAllAsync()
        {
            return await _context.StatusJobs.ToListAsync();
        }
        public async Task<StatusJob> GetByIdAsync(int Id)
        {
            return await _context.StatusJobs.FindAsync(Id);
        } 
        public async Task AddAsync(StatusJob statusJob)
        {
            _context.StatusJobs.Add(statusJob);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(StatusJob statusJob)
        {
            _context.StatusJobs.Update(statusJob);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var statusJob = await _context.StatusJobs.FindAsync(id);
            _context.StatusJobs.Remove(statusJob);
            await _context.SaveChangesAsync();
        }

        public List<StatusJob> GetListJobByEmail(string email)
        {
            return _context.StatusJobs.Where(x => x.Email == email).ToList();
        } 
        
        public List<string> GetJobnameByID(List<int> JobID)
        {

            var jobTitles = new List<string>();


            foreach (var jobId in JobID )
            {
                var job = _context.Jobs.FirstOrDefault(j => j.Id == jobId);
                if (job != null)
                {
                    jobTitles.Add(job.Title);
                }
            }

            return jobTitles;

            //var jobname =  _context.Jobs.FirstOrDefault(p => p.Id == JobID);

            //return jobname()
        }
    }
}
