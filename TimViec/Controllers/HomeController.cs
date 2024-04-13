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
			var companies = await _companyRepository.GetAllAsync();
			return View(companies);
		}
		public IActionResult Profile()
        {
            return View();
        }

		public IActionResult Details_Company()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
