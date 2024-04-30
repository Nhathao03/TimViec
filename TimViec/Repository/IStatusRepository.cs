using TimViec.Models;

namespace TimViec.Repository
{
    public interface IStatusRepository
    {
        Task<IEnumerable<StatusJob>> GetAllAsync();
        Task<StatusJob> GetByIdAsync(int id); 
        Task AddAsync(StatusJob statusJob);
        Task UpdateAsync(StatusJob statusJob);
        Task DeleteAsync(int id);
        List<StatusJob> GetListJobByEmail(string email);

        List<string> GetJobnameByID(List<int> JobID);
    }
}
