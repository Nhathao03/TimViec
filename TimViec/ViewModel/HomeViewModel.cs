using TimViec.Models;

namespace TimViec.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<Job> Jobs { get; set; }
        public IEnumerable<Company> Companies { get; set; }
    }
}
