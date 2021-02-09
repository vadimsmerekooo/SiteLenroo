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
    public class CategorysModel : PageModel
    {

        [TempData]
        public string StatusMessage { get; set; }

        public SiteLenrooContext2 _context = new SiteLenrooContext2();

        public List<AspNetCategory> categoryes;

        public CategorysModel()
        {
            categoryes = _context.AspNetCategory.ToList();
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnGetDeleteAsync(int catId)
        {
            if (_context.AspNetCategory.Any(c => c.Id == catId) && _context.AspNetNews.Count(n => n.Category == catId) == 0)
            {
                _context.AspNetCategory.Remove(categoryes.FirstOrDefault(c => c.Id == catId));
                _context.SaveChanges();
                categoryes = _context.AspNetCategory.ToList();
                StatusMessage = "Категория успешно удалена!";
                return Page();
            }
            StatusMessage = "Ошибка при удалении категории!";
            return Page();
        }
    }
}
