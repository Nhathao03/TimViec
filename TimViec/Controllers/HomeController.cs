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

		private async Task DisplayDropdown()
		{
			var rank = await _rankRespository.GetAllAsync();
			var skill = await _skillRespository.GetAllAsync();
			var type_work = await _WorkRespository.GetAllAsync();
			var city = await _cityRespository.GetAllAsync();

			ViewBag.Skill = skill.ToList();
			ViewBag.Type = type_work.ToList();
			ViewBag.Rank = rank.ToList();
			ViewBag.Location = city.ToList();
		}

		// display item
		public async Task<IActionResult> Index()
		{
			var jobs = await _jobRepository.GetAllAsync();
			var companies = await _companyRepository.GetAllAsync();

			await DisplayDropdown();

			// get list companies

			//take n = 6 item in table job
			jobs = jobs.Take(6);

			companies = companies.Take(3);
			HomeViewModel home = new HomeViewModel()
			{
				Jobs = jobs,
				Companies = companies,
			};

			return View(home);
		}

        //**********************************************************************************************
        //status job
        public IActionResult StJ()
		{
            var name = User.Identity.Name;
			var status = _statusRepository.GetListJobByEmail(email: name);

			foreach(var job in status)
			{
				job.StatusName = EnumExtension.GetEnumDescription((Constants.StatusJob)job.Status);
			}

			return View(status);
		}
		
        // Delete Status
        public async Task<IActionResult> Delete_Status(int ID)
         {
            await DisplayDropdown();
			var status = await _statusRepository.GetByIdAsync(ID);
			var getjobname = await _jobRepository.GetByIdAsync(status.JobID);
			var companyname = _companyRepository.Details_CPN(getjobname.CompanyID).FirstOrDefault();

			ViewBag.CompanyName = companyname;
			ViewBag.Jobname = getjobname.Title;

            if (status == null) 
            {
                return NotFound();
            }
            return View(status);
        }


        // Process delete status
        [HttpPost, ActionName("Delete_Status")]
        public async Task<IActionResult> DeleteConfirmed_Company(int id)
        {
            await _statusRepository.DeleteAsync(id);
            return RedirectToAction(nameof(StJ));

        }

        //**********************************************************************************************

        //all Job                                 
        public async Task<IActionResult> Job()
		{

			await DisplayDropdown();

			var job = await _jobRepository.GetAllAsync();

			return View(job);
		}

		//all company
		public async Task<IActionResult> Company()
		{
            await DisplayDropdown();

            var companies = await _companyRepository.GetAllAsync();
            return View(companies);
		}

        //**********************************************************************************************
        // search
        [HttpGet]
		public async Task<IActionResult> Search(string stringSearch)
		{
            await DisplayDropdown();
            var result = _jobRepository.Search(stringSearch);

			return View(result);
		}
        //**********************************************************************************************
        // display details company
        public async Task<IActionResult> Details_CPN(int id)
		{
            await DisplayDropdown();

            var result = _companyRepository.Details_CPN(id);
			if (result == null)
			{
				return NotFound();
			}
			return View(result);
		}

		//details job     
		public async Task<IActionResult> Details_Job(int id)
		{
            await DisplayDropdown();

            var job = _jobRepository.Details_Job(id);
			if (job == null)
			{
				return NotFound();
			}
			return View(job);
		}

		//***************************************************************************************
		//create applications
		public async Task<IActionResult> CreateApplication(int id)
		{
            await DisplayDropdown();
			var user = await _userManager.GetUserAsync(User);
			ViewBag.Email = user.Email;
            var status = await _statusRepository.GetAllAsync();

			var job = await _jobRepository.GetByIdAsync(id);
			ViewBag.GetJob = job;
			
            if (status == null)
            {
                return NotFound();
            }
            return View();
		}
		// add status job
		[HttpPost]
		public async Task<IActionResult> CreateApplication(StatusJob statusJob, IFormFile imgCV, int id)
		{
            statusJob.ID = 0;
			statusJob.JobID = id;
			if (imgCV != null)
			{
				// save imgae
				statusJob.imgCV = await SaveImage(imgCV);
			}

            statusJob.Status = (int)Constants.StatusJob.Inprogress;
            statusJob.Read = (int)Constants.ViewStatus.NoRead;

            await _statusRepository.AddAsync(statusJob);
			return RedirectToAction(nameof(StJ));

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

		//**********************************************************************************************
		//Search in Dropdown
		public async Task<IActionResult> ChoeseSearchSkill(int ID)
		{
            await DisplayDropdown();

            var skill = _jobRepository.ChoeseSearchSkills(ID);

			return View(skill);
		}

		public async Task<IActionResult> ChoeseSearchType(int ID)
		{
            await DisplayDropdown();

            var type = _jobRepository.ChoeseSearchType(ID);

			return View(type);
		}

		public async Task<IActionResult> ChoeseSearchRank(int ID)
		{
            await DisplayDropdown();

            var rank = _jobRepository.ChoeseSearchRank(ID);

			return View(rank);
		}

		public async Task<IActionResult> ChoeseSearchLocation(int ID)
		{
            await DisplayDropdown();

            var location = _jobRepository.ChoeseSearchLocation(ID);

			return View(location);
		}
		//*******************************************************************************************
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
