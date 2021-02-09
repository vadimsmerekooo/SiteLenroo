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
    [Authorize(Roles = "admin, moderator")]
    public class AddedTagModel : PageModel
    {

        [TempData]
        public string StatusMessage { get; set; }

        SiteLenrooContext2 _context = new SiteLenrooContext2();

        public async Task<IActionResult> OnPostCreateAsync(string tag)
        {
            if (!string.IsNullOrEmpty(tag))
            {
                if(!_context.AspNetTag.Any(t => t.Tag == tag))
                {
                    AspNetTag aspNetTag = new AspNetTag()
                    {
                        Tag = tag
                    };
                    _context.AspNetTag.Add(aspNetTag);
                    _context.SaveChanges();
                    StatusMessage = "Тег успешно добавлен!";
                    return RedirectToPage("./Tags");
                }
                else
                {
                    StatusMessage = "Тэг с таким же названием уже существует!";
                    return Page();
                }
            }
            StatusMessage = "Ошибка при добавлении тега!";
            return Page();
        }

        public void OnGet()
        {
        }
    }
}
