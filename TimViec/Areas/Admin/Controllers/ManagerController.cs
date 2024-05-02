using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public ManagerController(ICompanyRespository companyRepository,
			IJobRespository jobRepository,
			IStatusRepository statusRepository,
			IApplicationUser userManager)
		{
			_jobRepository = jobRepository;
			_companyRepository = companyRepository;
            _statusRepository = statusRepository;
            _applicationUser = userManager;
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


        //account user
        public async Task<IActionResult> Account_Admin(string Id)
        {
            var MAdmin = await _applicationUser.GetByIdAsync(Id);

			if (MAdmin == null)
			{
				return NotFound();
			}

			return View(MAdmin);
        }

		// Process the product update
		[HttpPost]
		public async Task<IActionResult> Account_Admin(ApplicationUser applicationUser)
		{
			if (ModelState.IsValid)
			{
				await _applicationUser.UpdateAsync(applicationUser);
				return RedirectToAction("Index");
			}
			return View(applicationUser);
		}



	}
}
