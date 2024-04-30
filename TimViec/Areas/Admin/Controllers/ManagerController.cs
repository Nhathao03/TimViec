using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TimViec.Models;
using TimViec.Repository;
using TimViec.Respository;

namespace TimViec.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles ="Admin")]
    public class ManagerController : Controller
	{
		private readonly IJobRespository _jobRepository;
		//private readonly ICompanyRespository _companyRepository;
		//private readonly IStatusRepository _statusRepository;
		//private readonly UserManager<ApplicationUser> _userManager;

		public ManagerController(ICompanyRespository companyRepository,
			IJobRespository jobRepository,
			IStatusRepository statusRepository,
			UserManager<ApplicationUser> userManager)
		{
			_jobRepository = jobRepository;
			//_companyRepository = companyRepository;
			//_statusRepository = statusRepository;
			//_userManager = userManager;
		}
        //all Job
        public async Task<IActionResult> Job()
        {
            var job = await _jobRepository.GetAllAsync();
            return View(job);
        }
    }
}
