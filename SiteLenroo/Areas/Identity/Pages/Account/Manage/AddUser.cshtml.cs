using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SiteLenroo.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "admin")]
    public class AddUserModel : PageModel
    {
        private readonly UserManager<LenrooUser> _userManager;

        public AddUserModel(
            UserManager<LenrooUser> userManager)
        {
            _userManager = userManager;
        }
        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Login")]
            public string Login { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [Display(Name = "Password")]
            public string Password { get; set; }
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAddedUserAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new LenrooUser { UserName = Input.Login, Email = Input.Login, EmailConfirmed = true};
                var result = await _userManager.CreateAsync(user, Input.Password + "Lbp2900!");
                if (result.Succeeded)
                {
                    return RedirectToPage("./UserList");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }
    }
}