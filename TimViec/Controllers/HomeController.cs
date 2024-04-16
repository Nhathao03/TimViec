using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TimViec.Models;
using TimViec.Respository;

namespace TimViec.Controllers
{
    public class HomeController : Controller
    {
		private readonly IJobRespository _jobRepository;
		private readonly ICompanyRespository _companyRepository;
		public HomeController(ICompanyRespository companyRepository, IJobRespository jobRepository)
		{
			_jobRepository = jobRepository;
			_companyRepository = companyRepository;
		}

		// display item
		public async Task<IActionResult> Index()
		{
			var job = await _jobRepository.GetAllAsync();
			return View(job);
		}
		public IActionResult Profile()
        {
            return View();
        }



        //details job
        public async Task<IActionResult> Details_Job(int id)
        {
            var job = await _jobRepository.GetByIdAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
