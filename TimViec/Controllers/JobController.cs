﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TimViec.Models;
using TimViec.Repository;
using TimViec.Respository;
using TimViec.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace TimViec.Controllers
{
	[Authorize]
	[Authorize(Roles = "User")]
	public class JobController : Controller
	{
		private readonly IJobRespository _jobRepository;
		private readonly ICompanyRespository _companyRepository;
		private readonly IStatusRepository _statusRepository;
		private readonly IRankRespository _rankRespository;
		private readonly ISkillRespository _skillRespository;
		private readonly IType_WorkRespository _WorkRespository;
		private readonly ICityRespository _cityRespository;
		private readonly IfavouriteJob _favouriteJob;
		private readonly IfeedbackRepository _feedbackRepository;
		private readonly UserManager<ApplicationUser> _userManager;

		public JobController(ICompanyRespository companyRepository,
			IJobRespository jobRepository,
			IStatusRepository statusRepository,
			IRankRespository rankRespository,
			ISkillRespository skillRespository,
			IType_WorkRespository type_workRespository,
			ICityRespository cityRespository,
			IfavouriteJob favouriteJob,
			IfeedbackRepository feedback,
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
			_favouriteJob = favouriteJob;
			_feedbackRepository = feedback;
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

		//all Job                                 
		public async Task<IActionResult> Job()
		{
			await DisplayDropdown();

			var job = await _jobRepository.GetAllAsync();
			return View(job);
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
		public async Task<IActionResult> GetAllFavouriteJob()
		{
			await DisplayDropdown();
			var getemailuser = await _userManager.GetUserAsync(User);

			var fbJob = await _favouriteJob.GetAllAsync();
			var display = fbJob.Where(e => e.email == getemailuser.Email);
			return View(display);
		}

		[HttpGet]
		public async Task<IActionResult> Favourite_Job(favourite_job favourite_Job, int ID)
		{
			var getemailuser = await _userManager.GetUserAsync(User);
			await DisplayDropdown();
			var getfvrjob = await _favouriteJob.GetAllAsync();

			var getID = await _jobRepository.GetByIdAsync(ID);
			favourite_Job.email = getemailuser.Email;
			favourite_Job.Id = 0;
			favourite_Job.IDJob = getID.Id;
			favourite_Job.Name = getID.Title;
			favourite_Job.R1_Language = getID.R1_Language;
			favourite_Job.R2_Language = getID.R2_Language;
			favourite_Job.R3_Language = getID.R3_Language;
			favourite_Job.image = getID.img;
			favourite_Job.favourite = (int)Constants.favouriteJob.favourite;

			await _favouriteJob.AddAsync(favourite_Job);
			return RedirectToAction("GetAllFavouriteJob"); ;
		}

		[HttpGet]
		public async Task<IActionResult> ChangeFavourite_Job(favourite_job favourite_Job, int ID)
		{
			await DisplayDropdown();
			await _favouriteJob.DeleteAsync(ID);
			return RedirectToAction("GetAllFavouriteJob"); ;
		}
	}
}
