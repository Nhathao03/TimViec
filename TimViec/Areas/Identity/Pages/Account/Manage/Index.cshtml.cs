﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TimViec.Models;

namespace TimViec.Areas.Identity.Pages.Account.Manage
{
    [Authorize]
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "Họ và tên")]
            public string Fullname { get; set; }

            [Display(Name = "Họ")]
            public string Firstname { get; set; }

            [Display(Name = "Tên")]
            public string Lastname { get; set; }

            [Display(Name = "Avatar")]
            public string? imgCV { get; set; }

            [Display(Name = "Ngày sinh")]
            public DateTime? Birth { get; set; }

            [Display(Name = "Kinh nghiệm")]
            public string? Experiance { get; set; }

            [Display(Name = "Avatar")]
            public string? avatar { get; set; }

            [Phone]
            [Display(Name = "Số điện thoại")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);


            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Fullname = user.Fullname,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                imgCV = user.imgCV,
                Birth = user.Birth,
                avatar = user.avatar,
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            //var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            //if (Input.PhoneNumber != phoneNumber)
            //{
            //    var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
            //    if (!setPhoneResult.Succeeded)
            //    {
            //        StatusMessage = "Unexpected error when trying to set phone number.";
            //        return RedirectToPage();
            //    }
            //}

            user.Firstname = Input.Firstname;
            user.Lastname = Input.Lastname;
            user.Fullname = Input.Fullname;
            user.PhoneNumber = Input.PhoneNumber;
          //  user.Birth = Input.Birth;
            user.avatar = Input.avatar;      
            user.imgCV = Input.imgCV;

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Hồ sơ của bạn đã được cập nhật thành công !";
            return RedirectToPage();
        }

        //// Process the product update
        //[HttpPost]
        //public async Task<IActionResult> Edit(ApplicationUser applicationUser, IFormFile imgCV)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (imgCV != null)
        //        {
        //            // Lưu hình ảnh đại diện
        //            applicationUser.imgCV = await SaveImage(imgCV);
        //        }
        //        await _userManager.UpdateAsync(applicationUser);
        //        return RedirectToAction("Index");
        //    }
        //    return View(applicationUser);
        //}

        ////Luu anh
        //private async Task<string> SaveImage(IFormFile image)
        //{
        //    var savePath = Path.Combine("wwwroot/images", image.FileName);
        //    using (var fileStream = new FileStream(savePath, FileMode.Create))
        //    {
        //        await image.CopyToAsync(fileStream);
        //    }
        //    return "/images/" + image.FileName; // Trả về đường dẫn tương đối
        //}
    }
}