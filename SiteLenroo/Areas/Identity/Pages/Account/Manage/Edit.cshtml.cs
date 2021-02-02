using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SiteLenroo.ViewModels;

namespace SiteLenroo.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "admin")]
    public class EditModel : PageModel
    {
        public ChangeRole changeRole;
        RoleManager<IdentityRole> _roleManager;
        UserManager<LenrooUser> _userManager;
        public EditModel(RoleManager<IdentityRole> roleManager, UserManager<LenrooUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> OnGetAsync(string userId)
        {
            // �������� ������������
            LenrooUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // ������� ������ ����� ������������
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                changeRole = new ChangeRole
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return Page();
            }

            return NotFound();
        }
        public async Task<IActionResult> OnPostEditAsync(string userId, List<string> roles)
        {
            // �������� ������������
            LenrooUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // ������� ������ ����� ������������
                var userRoles = await _userManager.GetRolesAsync(user);
                // �������� ��� ����
                var allRoles = _roleManager.Roles.ToList();
                // �������� ������ �����, ������� ���� ���������
                var addedRoles = roles.Except(userRoles);
                // �������� ����, ������� ���� �������
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToPage("./UserList");
            }

            return NotFound();
        }
    }
}