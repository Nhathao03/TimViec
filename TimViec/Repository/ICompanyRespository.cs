using TimViec.Models;
using TimViec.ViewModel;

namespace TimViec.Respository
{
    public interface ICompanyRespository
    {
        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company> GetByIdAsync(int id);
        Task AddAsync(Company company);
        Task UpdateAsync(Company company);
        Task DeleteAsync(int id);
        
    }
}
