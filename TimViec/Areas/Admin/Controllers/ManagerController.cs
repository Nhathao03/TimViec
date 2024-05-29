﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;
using TimViec.Models;
using TimViec.Repository;
using TimViec.Respository;
using TimViec.ViewModel;

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
        private readonly IType_WorkRespository _WorkRespository;
        private readonly IfeedbackRepository _feedback;
        private readonly UserManager<ApplicationUser> _userManagers;

		public ManagerController(ICompanyRespository companyRepository,
			IJobRespository jobRepository,
			IStatusRepository statusRepository,
			IApplicationUser userManager,
            IType_WorkRespository type_workRespository,
            IfeedbackRepository feedback,
            UserManager<ApplicationUser> userManagers)
		{
			_jobRepository = jobRepository;
			_companyRepository = companyRepository;
            _statusRepository = statusRepository;
            _applicationUser = userManager;
			_userManagers = userManagers;
            _WorkRespository = type_workRespository;
            _feedback = feedback;
        }

        //Index
        public async Task<IActionResult> Index()
        {
            var CountCompany = await _companyRepository.GetAllAsync();
            int CountC = (from c in CountCompany select c.Id).Count();

            string role = "User";
            var CountUser = _applicationUser.GetAllUser(role);
            int CountU = (from u in CountUser select u.email).Count();

            var admin = await _userManagers.GetUserAsync(User);
            ViewBag.Admin = admin.Fullname;

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

            ViewBag.Rank = "abc";
            if(job.RankID == 1)
            {
                ViewBag.Rank = "Intern";
            }
            else if(job.RankID == 2)
            {
                ViewBag.Rank = "Fresher";
            }
            else if (job.RankID == 3)
            {
                ViewBag.Rank = "Junior";
            }
            else if (job.RankID == 4)
            {
                ViewBag.Rank = "Middle";
            }
            else if (job.RankID == 5)
            {
                ViewBag.Rank = "Senior";
            }
            else if (job.RankID == 6)
            {
                ViewBag.Rank = "Trưởng nhóm";
            }
            else if (job.RankID == 7)
            {
                ViewBag.Rank = "Trưởng phòng";
            }
            else
            {
                ViewBag.Rank = "All Levels";
            }

            ViewBag.Type = "abc";
            if (job.Type_workID == 1)
            {
                ViewBag.Type = "In Office";
            }
            else if (job.Type_workID == 2)
            {
                ViewBag.Type = "Hybird";
            }
            else if (job.Type_workID == 3)
            {
                ViewBag.Type = "Remote";
            }
            else 
            {
                ViewBag.Type = "Oversea";
            }

            if(job.R1_Language == null)
            {
                job.R1_Language = "Không yêu cầu";
            }
            else if(job.R1_Language == null)
            {
                job.R2_Language = "Không yêu cầu";
            }
            else if(job.R3_Language == null)
            {
                job.R3_Language = "Không yêu cầu";
            }

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
            
            return RedirectToAction(nameof(Job));

        }
        //***************************************************************************
        //Hiển thị tất cả công ty
        public async Task<IActionResult> Company()
        {

            var companies = await _companyRepository.GetAllAsync();

            return View(companies);
        }

        // Xử lý xóa sản phẩm
        [HttpGet]
        public async Task<IActionResult> DeleteConfirmed_Company(int id)
        {
            await _companyRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Account_Company));

        }
        //***************************************************************************


        //account admin
        public async Task<IActionResult> Account_Admin()
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
			if (ModelState.IsValid)
			{

				await _applicationUser.UpdateAsync(applicationUser);
				return RedirectToAction("Account_Admin");
			}
			return View(applicationUser);
		}

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
        //all account user
        public async Task<IActionResult> Account_User(string role)
        {
            role = "User";
            var result = _applicationUser.GetAllUser(role);

            return View(result);
        }

        // Delete user
        public async Task<IActionResult> Delete_User(string ID)
        {
           var user  = await _applicationUser.GetByStringId(ID);

            ViewBag.user = user.Fullname;
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // Xử lý xóa
        [HttpPost, ActionName("Delete_User")]
        public async Task<IActionResult> DeleteConfirmed_User(string id)
        {
            await _applicationUser.DeleteStringAsync(id);
            return RedirectToAction(nameof(Account_User));

        }

        //*********************************************************************************************
        //all account company
        public async Task<IActionResult> Account_Company(string role)
        {
            
            role = "Company";
            var result = _applicationUser.GetAllCompany(role);


            return View(result);
        }

        // Delete company
        public async Task<IActionResult> Delete_Company(string ID)
        {
            var user = await _applicationUser.GetByStringId(ID);

            var company = await _companyRepository.GetByEmailAsync(user.Email);

            ViewBag.name = company.Name_company;
            ViewBag.Size = company.Company_size;
            ViewBag.Type = company.Company_type;
            ViewBag.Location = company.Location;

            ViewBag.user = user.Fullname;
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // Xử lý xóa sản phẩm
        [HttpPost, ActionName("Delete_Company")]
        public async Task<IActionResult> DeleteConfirmed_Company(string id)
        {
            await _applicationUser.DeleteStringAsync(id);
            return RedirectToAction(nameof(Account_Company));

        }

        //get feedback
        public async Task<IActionResult> Getfeedback()
        {
            var result = await _feedback.GetAllAsync();
            return View(result);
        }

		[HttpGet]
		public async Task<IActionResult> DetailsFeedback(int ID)
		{
			var getfeedback = await _feedback.GetByIdAsync(ID);
            ViewBag.NameUserFeedback = getfeedback.Name;
            ViewBag.NoteUserFeedback = getfeedback.Note;
            if (getfeedback == null)
			{
				return NotFound();
			}
			return View(getfeedback);

		}

		public async Task<IActionResult> Delete_Feedback(int ID)
        {
            await _feedback.DeleteAsync(ID);
            return RedirectToAction(nameof(Getfeedback));
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(EmailData data, int ID)
        {

            data.From = "nhathaoha11@gmail.com";

            //lay du lieeu de hien thi
            var getNameUser = await _feedback.GetByIdAsync(ID);
            data.Body = System.IO.File.ReadAllText("C:/Users/yukun/source/repos/TimViec/TimViec/wwwroot/Template/feedback.html");
            data.Body = data.Body.Replace("{{UserName}}", getNameUser.Name)
                                 .Replace("{{Note}}", getNameUser.Note);

            var result = await SendMail.SendGmail(data.From, data.To, data.Subject, data.Body, "nhathaoha11@gmail.com", "gheh wppp gokl rmrn");

            return RedirectToAction(nameof(Getfeedback)); ;
        }
    }
}
