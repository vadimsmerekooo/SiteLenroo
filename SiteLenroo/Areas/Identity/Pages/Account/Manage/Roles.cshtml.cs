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
    public class RolesModel : PageModel
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<LenrooUser> _userManager;
        public List<IdentityRole> _roles;
        public RolesModel(RoleManager<IdentityRole> roleManager, UserManager<LenrooUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _roles = _roleManager.Roles.ToList();
        }
        [TempData]
        public string StatusMessage { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    StatusMessage = "Роль успешно удалена!";
                else
                    StatusMessage = "Ошибка. Роль не удалена!";
            }
            return RedirectToPage("./Roles");
        }
    }
}