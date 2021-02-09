using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SiteLenroo.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "admin, moderator")]
    public class AddedCategoryModel : PageModel
    {

        [TempData]
        public string StatusMessage { get; set; }


        SiteLenrooContext2 _context = new SiteLenrooContext2();

        public async Task<IActionResult> OnPostCreateAsync(string cat)
        {
            if (!string.IsNullOrEmpty(cat))
            {
                if (!_context.AspNetCategory.Any(c => c.Category == cat))
                {
                    AspNetCategory aspNetCategory = new AspNetCategory()
                    {
                        Category= cat
                    };
                    _context.AspNetCategory.Add(aspNetCategory);
                    _context.SaveChanges();
                    StatusMessage = "Категория успешно добавлена!";
                    return RedirectToPage("./Categorys");
                }
                else
                {
                    StatusMessage = "Категория с таким же названием уже существует!";
                    return Page();
                }
            }
            StatusMessage = "Ошибка при добавлении категории!";
            return Page();
        }
        public void OnGet()
        {
        }
    }
}
