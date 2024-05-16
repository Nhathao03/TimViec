using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;
using TimViec.Helpers;
using TimViec.Models;
using TimViec.Repository;
using TimViec.Respository;

namespace TimViec.Areas.CompanyManage.Controllers
{
    [Area("CompanyManage")]
    [Authorize(Roles = "Company")]
    public class CompanyController : Controller
    {
        private readonly IJobRespository _jobRepository;
        private readonly ICompanyRespository _companyRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IApplicationUser _applicationUser;
        private readonly IType_WorkRespository _WorkRespository;
        private readonly IRankRespository _rankRespository;
        private readonly ISkillRespository _skillRespository;
        private readonly ICityRespository _cityRespository;
        private readonly UserManager<ApplicationUser> _userManagers;

        public CompanyController(ICompanyRespository companyRepository,
            IJobRespository jobRepository,
            IStatusRepository statusRepository,
            IApplicationUser userManager,
            IRankRespository rankRespository,
            ISkillRespository skillRespository,
            IType_WorkRespository type_workRespository,
            ICityRespository cityRespository,
            UserManager<ApplicationUser> userManagers)
        {
            _jobRepository = jobRepository;
            _companyRepository = companyRepository;
            _statusRepository = statusRepository;
            _applicationUser = userManager;
            _userManagers = userManagers;
            _WorkRespository = type_workRespository;
            _cityRespository = cityRespository;
            _skillRespository = skillRespository;
            _rankRespository = rankRespository;
        }

        //trang chu
        public async Task<IActionResult> Index()
        {
            var user = await _userManagers.GetUserAsync(User);
            string us = user.Email;

            //dem so luong cong viec
            var getinfor = _applicationUser.GetInforCompany(us);

            int id = getinfor.Select(x => x.Id).FirstOrDefault();
            var Count = _companyRepository.CountJobInCompanies(id).Count();

            var countstatus = _statusRepository.CompanyCheckStatus(user.Email).Count();

            ViewBag.CountStatus = countstatus;
            ViewBag.Count = Count;

            return View(getinfor);
        }

        //**********************************************************************************************
        //Edit company
        public async Task<IActionResult> Edit_company()
        {
            var user = await _userManagers.GetUserAsync(User);

            var getinfor = await _companyRepository.GetByEmailAsync(user.Email);

            var editCPNa = await _companyRepository.GetByIdAsync(getinfor.Id);
            var type_work = await _WorkRespository.GetAllAsync();
            ViewBag.Type = new SelectList(type_work, "Id", "Type");  

            var city = await _cityRespository.GetAllAsync();
            ViewBag.City = new SelectList(city, "Id", "Name_city");

            return View(editCPNa);
        }

        // Process the product update
        [HttpPost]
        public async Task<IActionResult> Edit_company(Company company, IFormFile Image)
        {

            if (Image != null)
            {
                company.Image = await SaveImageEdit(Image);
            }
            
            await _companyRepository.UpdateAsync(company);

            return RedirectToAction("Edit_company");
        }

        //**********************************************************************************************
        //Luu anh
        private async Task<string> SaveImageEdit(IFormFile Image)
        {
            var savePath = Path.Combine("wwwroot/LayoutTimViec/img", Image.FileName);
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await Image.CopyToAsync(fileStream);
            }
            return Image.FileName;
        }

        //**********************************************************************************************
        // display details company
        public async Task<IActionResult> AllJob()
        {
            var user = await _userManagers.GetUserAsync(User);
            string email = user.Email;
            var result = _companyRepository.GetJobByEmail(email);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

        // Delete Job
        public async Task<IActionResult> Delete_Job(int ID)
        {
            var result = await _jobRepository.GetByIdAsync(ID);

            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }


        // Process delete job
        [HttpPost, ActionName("Delete_Job")]
        public async Task<IActionResult> DeleteConfirmed_Job(int id)
        {
            await _jobRepository.DeleteAsync(id);
            return RedirectToAction(nameof(AllJob));

        }
        //**********************************************************************************************
        // display details company
        public async Task<IActionResult> Create()
        {
            var rank = await _rankRespository.GetAllAsync();
            var skill = await _skillRespository.GetAllAsync();
            var type_work = await _WorkRespository.GetAllAsync();


            ViewBag.Skill = new SelectList(skill, "Id", "Skills");
            ViewBag.Type = new SelectList(type_work, "Id", "Type");
            ViewBag.Rank = new SelectList(rank, "Id", "rank");


            return View();
        }
        //Luu anh
        private async Task<string> SaveImage(IFormFile img)
        {
            var savePath = Path.Combine("wwwroot/LayoutTimViec/img", img.FileName);
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await img.CopyToAsync(fileStream);
            }
            return img.FileName;
        }

        // create 
        [HttpPost]
        public async Task<IActionResult> Create(Job job, IFormFile img)
        {

            var company = await _userManagers.GetUserAsync(User);
            var idcompany = await _companyRepository.GetByEmailAsync(company.Email);

            job.CompanyID = idcompany.Id;

            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    job.img = await SaveImage(img);  
                }

                await _jobRepository.AddAsync(job);
                return RedirectToAction(nameof(Index));
            }
             return View(job);
        }

        //*************************************************************************************
        //Applications Company
        public async Task<IActionResult> CompanyCheckStatus()
        {
            var user = await _userManagers.GetUserAsync(User);

            var result = _statusRepository.CompanyCheckStatus(user.Email);

            foreach (var job in result)
            {
                job.Statusname = EnumExtension.GetEnumDescription((Constants.StatusJob)job.Status);
            }

            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

    }
}
