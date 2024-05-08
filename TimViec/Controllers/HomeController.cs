using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using TimViec.Helpers;
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
		private readonly IRankRespository _rankRespository;
		private readonly ISkillRespository _skillRespository;
		private readonly IType_WorkRespository _WorkRespository;
		private readonly ICityRespository _cityRespository;
		private readonly UserManager<ApplicationUser> _userManager;

		public HomeController(ICompanyRespository companyRepository,
			IJobRespository jobRepository,
			IStatusRepository statusRepository,
			IRankRespository rankRespository,
			ISkillRespository skillRespository,
			IType_WorkRespository type_workRespository,
			ICityRespository cityRespository,
			UserManager<ApplicationUser> userManager)
		{
			_jobRepository = jobRepository;
			_companyRepository = companyRepository;
			_statusRepository = statusRepository;
			_userManager = userManager;
			_skillRespository = skillRespository;
			_WorkRespository = type_workRespository;
			_cityRespository = cityRespository;
			_rankRespository = rankRespository;
		}

		// display item
		public async Task<IActionResult> Index()
		{
			var jobs = await _jobRepository.GetAllAsync();
			var companies = await _companyRepository.GetAllAsync();
			var rank = await _rankRespository.GetAllAsync();
			var skill = await _skillRespository.GetAllAsync();
			var type_work = await _WorkRespository.GetAllAsync();
			var city = await _cityRespository.GetAllAsync();


			// get list companies

			//take n = 6 item in table job
			jobs = jobs.Take(6);

			companies = companies.Take(3);
			HomeViewModel home = new HomeViewModel()
			{
				Jobs = jobs,
				Companies = companies,
				Cities = city,
				ranks = rank,
				Type_Works = type_work,
				skills = skill,
			};


			ViewBag.Skill = skill.Select(s => s.Skills).ToList();
			ViewBag.Type = type_work.Select(t => t.Type).ToList();
			ViewBag.Rank = rank.Select(r => r.rank).ToList();
			ViewBag.Location = city.Select(c => c.Name_city).ToList();

			return View(home);
		}

		//status job
		public IActionResult StJ()
		{
			var name = User.Identity.Name;
			var status = _statusRepository.GetListJobByEmail(email: name);


			return View(status);
		}


		//all Job
		public async Task<IActionResult> Job()
		{
            var rank = await _rankRespository.GetAllAsync();
            var skill = await _skillRespository.GetAllAsync();
            var type_work = await _WorkRespository.GetAllAsync();
            var city = await _cityRespository.GetAllAsync();

            ViewBag.Skill = skill.Select(s => s.Skills).ToList();
            ViewBag.Type = type_work.Select(t => t.Type).ToList();
            ViewBag.Rank = rank.Select(r => r.rank).ToList();
            ViewBag.Location = city.Select(c => c.Name_city).ToList();

            var job = await _jobRepository.GetAllAsync();
			return View(job);
		}

		//all company
		public async Task<IActionResult> Company()
		{
            var rank = await _rankRespository.GetAllAsync();
            var skill = await _skillRespository.GetAllAsync();
            var type_work = await _WorkRespository.GetAllAsync();
            var city = await _cityRespository.GetAllAsync();

            ViewBag.Skill = skill.Select(s => s.Skills).ToList();
            ViewBag.Type = type_work.Select(t => t.Type).ToList();
            ViewBag.Rank = rank.Select(r => r.rank).ToList();
            ViewBag.Location = city.Select(c => c.Name_city).ToList();
            var companies = await _companyRepository.GetAllAsync();
			return View(companies);
		}

		// search
		[HttpGet]
		public async Task<IActionResult> Search(string stringSearch)
		{
			var result = _jobRepository.Search(stringSearch);

			return View(result);
		}

		// display item
		public async Task<IActionResult> Details_CPN(int id)
		{
			var result = _jobRepository.Details_CPN(id);
			if (result == null)
			{
				return NotFound();
			}
			return View(result);
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

		//***************************************************************************************
		//all Job
		public async Task<IActionResult> CreateApplication()
		{
			var status = await _statusRepository.GetAllAsync();
            if (status == null)
            {
                return NotFound();
            }
            return View();
		}
		// add status job
		[HttpPost]
		public async Task<IActionResult> CreateApplication(StatusJob statusJob, IFormFile imgCV)
		{
			if (imgCV != null)
			{
				// save imgae
				statusJob.imgCV = await SaveImage(imgCV);
			}

			statusJob.Status = (int)Constants.StatusJob.Inprogress;
			await _statusRepository.AddAsync(statusJob);
			return RedirectToAction(nameof(Index));

			return View(statusJob);
		}

		//Luu anh
		private async Task<string> SaveImage(IFormFile image)
		{
			var savePath = Path.Combine("wwwroot/LayoutTimViec/img", image.FileName);
			using (var fileStream = new FileStream(savePath, FileMode.Create))
			{
				await image.CopyToAsync(fileStream);
			}
			return "LayoutTimViec/img/" + image.FileName;
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
