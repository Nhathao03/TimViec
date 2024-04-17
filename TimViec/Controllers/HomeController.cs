using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TimViec.Models;
using TimViec.Repository;
using TimViec.Respository;

namespace TimViec.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
		private readonly IJobRespository _jobRepository;
		private readonly ICompanyRespository _companyRepository;
		private readonly IStatusRepository _statusRepository;

		public HomeController(ICompanyRespository companyRepository, IJobRespository jobRepository, IStatusRepository statusRepository)
		{
			_jobRepository = jobRepository;
			_companyRepository = companyRepository;
			_statusRepository = statusRepository;
		}

        // display item
        public async Task<IActionResult> Index()
        {
			var job = await _jobRepository.GetAllAsync();
			return View(job);
		}

		public async Task<IActionResult> CheckST()
        {
            var status = await _statusRepository.GetAllAsync();
            return View(status); ;
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
      
      
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
