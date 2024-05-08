using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Data;
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
		private readonly ICompanyRespository _companyRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IApplicationUser _applicationUser;
		private readonly UserManager<ApplicationUser> _userManagers;

		public ManagerController(ICompanyRespository companyRepository,
			IJobRespository jobRepository,
			IStatusRepository statusRepository,
			IApplicationUser userManager,
			UserManager<ApplicationUser> userManagers)
		{
			_jobRepository = jobRepository;
			_companyRepository = companyRepository;
            _statusRepository = statusRepository;
            _applicationUser = userManager;
			_userManagers = userManagers;
		}

        //Index
        public async Task<IActionResult> Index()
        {
            var CountCompany = await _companyRepository.GetAllAsync();
            int CountC = (from c in CountCompany select c.Id).Count();

            string role = "User";
            var CountUser = _applicationUser.GetAllUser(role);
            int CountU = (from u in CountUser select u.email).Count();

            //var MiddleSalary = await _jobRepository.GetAllAsync();
            //int CountS = ( from s in MiddleSalary select s.Salary).Count();
            //for ( int i = 0; i< CountS; i++)
            //{
            //    MiddleSalary.
            //}

            ViewBag.CountCompany = CountC;
            ViewBag.CountUser = CountU;
            return View();
        }

        //Hiển thị tất cả công việc
        public async Task<IActionResult> Job()
        {
            var job = await _jobRepository.GetAllAsync();
            return View(job);
        }

        // Hiển thị form xác nhận xóa công việc
        public async Task<IActionResult> Delete_Job(int id)
        {
            var job = await _jobRepository.GetByIdAsync(id);

            if (job == null)

            {
                return NotFound();
            }
            return View(job);
        }

        // Xử lý xóa sản phẩm
        [HttpPost, ActionName("Delete_Job")]
        public async Task<IActionResult> DeleteConfirmed_Job(int id)
        {
            await _jobRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));

        }
        //***************************************************************************
        //Hiển thị tất cả công ty
        public async Task<IActionResult> Company()
        {

            var companies = await _companyRepository.GetAllAsync();
            return View(companies);
        }

        // Hiển thị form xác nhận xóa công ty
        public async Task<IActionResult> Delete_Company(int id)
        {
            var company = await _companyRepository.GetByIdAsync(id);

            if (company == null)

            {
                return NotFound();
            }
            return View(company);
        }

        // Xử lý xóa sản phẩm
        [HttpPost, ActionName("Delete_Company")]
        public async Task<IActionResult> DeleteConfirmed_Company(int id)
        {
            await _companyRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Job));

        }
        //***************************************************************************


        //account admin
        public async Task<IActionResult> Account_Admin(string Id)
        {
            
            var user = await _userManagers.GetUserAsync(User);

			if (user == null)
			{
				return NotFound();
			}

			return View(user);
        }

		// Process the product update
		[HttpPost]
		public async Task<IActionResult> Account_Admin(ApplicationUser applicationUser)
		{
  //          if (avatar != null)
  //          {
  //              // Lưu hình ảnh đại diện
  //              applicationUser.avatar = await SaveImage(avatar);
  //          }
            await _applicationUser.UpdateAsync(applicationUser);
            return RedirectToAction("Account_Admin");
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

        //*********************************************************************************************
        //account user
        public async Task<IActionResult> Account_User(string role)
        {
            role = "User";
            var result = _applicationUser.GetAllUser(role);

            return View(result);
        }

    }
}
