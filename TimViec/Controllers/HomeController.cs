using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TimViec.Models;
using TimViec.Repository;
using TimViec.Respository;
using TimViec.ViewModel;

namespace TimViec.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
		private readonly IJobRespository _jobRepository;
		private readonly ICompanyRespository _companyRepository;
		private readonly IStatusRepository _statusRepository;

        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ICompanyRespository companyRepository, 
            IJobRespository jobRepository, 
            IStatusRepository statusRepository,
            UserManager<ApplicationUser> userManager)
		{
			_jobRepository = jobRepository;
			_companyRepository = companyRepository;
			_statusRepository = statusRepository;
            _userManager = userManager;
		}

        // display item
        public async Task<IActionResult> Index()
        {
			var jobs = await _jobRepository.GetAllAsync();
			var companies = await _companyRepository.GetAllAsync();
            // get list companies

            //take n = 6 item in table job
            jobs = jobs.Take(6);
            
            companies = companies.Take(3);
            HomeViewModel home = new HomeViewModel()
            {
                Jobs = jobs,
                Companies = companies
            };

			return View(home);
		}

		////status job
		//public async Task<IActionResult> CheckST()
  //      {
  //          var status = await _statusRepository.GetAllAsync();
  //          return View(status); 
  //      }
        
        //status job
		public IActionResult StJ()
        {
            var name = User.Identity.Name;
            var status = _statusRepository.GetListJobByEmail(email: name);
            return View(status); 
        }


        //details job     
        public async Task<IActionResult> Details_Job(int id)
        {
            var job = await _jobRepository.GetByIdAsync(id);
            Global.id_job = Convert.ToString(job.Id);
            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }

		//details company     
		public async Task<IActionResult> Details_Company(int id)
		{
			var company = await _companyRepository.GetByIdAsync(id);
			if (company == null)
			{
				return NotFound();
			}
			return View(company);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
