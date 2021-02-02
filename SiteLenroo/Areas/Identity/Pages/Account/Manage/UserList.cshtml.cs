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
    public class UserListModel : PageModel
    {
        UserManager<LenrooUser> _userManager;
        public List<LenrooUser> _userList;
        public UserListModel(UserManager<LenrooUser> userManager)
        {
            _userManager = userManager;
            _userList = _userManager.Users.ToList();
        }
        [TempData]
        public string StatusMessage { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnGetDeleteAsync(string userId)
        {
            LenrooUser user = await _userManager.FindByIdAsync(userId);
            if(user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                    StatusMessage = "Пользователь успешно удален";
                else
                    StatusMessage = " Ошибка. Пользователь не удален.";
            }
            return RedirectToPage("./UserList");
        }
    }
}