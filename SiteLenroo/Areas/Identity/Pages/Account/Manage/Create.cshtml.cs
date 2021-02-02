using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SiteLenroo.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "admin")]
    public class CreateModel : PageModel
    {
        public string nameRole;
        RoleManager<IdentityRole> _roleManager;
        UserManager<LenrooUser> _userManager;
        public List<IdentityRole> _roles;

        public CreateModel(RoleManager<IdentityRole> roleManager, UserManager<LenrooUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _roles = _roleManager.Roles.ToList();
        }
        public string StatusMessage { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostCreateAsync(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    StatusMessage = "Роль успешно созданна!";
                    return RedirectToPage("./Roles");
                }
                else
                {
                    StatusMessage = "Ошибка. Роль не созданна!";
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return RedirectToPage("./Roles");
        }
    }
}