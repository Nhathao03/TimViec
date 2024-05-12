using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly ICityRespository _cityRespository;
        private readonly UserManager<ApplicationUser> _userManagers;

        public CompanyController(ICompanyRespository companyRepository,
            IJobRespository jobRepository,
            IStatusRepository statusRepository,
            IApplicationUser userManager,
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

            ViewBag.Count = Count;
            if (getinfor == null)
            {
                return NotFound();
            }
            return View(getinfor);
        }


        //Edit company
        public async Task<IActionResult> Edit_company(string email)
        {
            var user = await _userManagers.GetUserAsync(User);
            email = user.Email;
            var getinfor = await _companyRepository.GetByEmailAsync(email);

            if (getinfor == null)
            {
                return NotFound();
            }

            var city = await _cityRespository.GetAllAsync();
            ViewBag.city = new SelectList(city, "Id", "city", getinfor.CityID);

            var type = await _WorkRespository.GetAllAsync();
            ViewBag.type = new SelectList(type, "Id", "Type", getinfor.Company_type);
            return View(getinfor);
        }

        // Process the product update
        [HttpPost]
        public async Task<IActionResult> Edit_company(Company company, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    company.Image = await SaveImage(Image);
                }
                await _companyRepository.UpdateAsync(company);
                return RedirectToAction("Index");
            }
            return View(company);
        }

        //Luu anh
        private async Task<string> SaveImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/Company/images", image.FileName);
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "Company/images/" + image.FileName; 
        }

        // display details company
        public async Task<IActionResult> AllJob(int id)
        {
            id = 3;
            var result = _companyRepository.Details_CPN(id);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

        // create 
        [HttpPost]
        public async Task<IActionResult> Create(Job job, IFormFile img)
        {
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    // Lưu hình ảnh đại diện
                    job.img = await SaveImage(img);
                }

                await _jobRepository.AddAsync(job);
                return RedirectToAction(nameof(Index));
            }
            
            var type = await _WorkRespository.GetAllAsync();
            ViewBag.type = new SelectList(type, "Id", "Type");

            var city = await _cityRespository.GetAllAsync();
            ViewBag.city = new SelectList(city, "Id", "Name_City");

            return View(job);
        }

    }
}
