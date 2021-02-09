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
    public class TagsModel : PageModel
    {

        [TempData]
        public string StatusMessage { get; set; }

        public SiteLenrooContext2 _context = new SiteLenrooContext2();

        public List<AspNetTag> tags;

        public TagsModel()
        {
            tags = _context.AspNetTag.ToList();
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnGetDeleteTagAsync(int tagId)
        {
            if(_context.AspNetTag.Any(t => t.Id == tagId) && _context.AspNetNews.Count(n => n.Tag == tagId) == 0)
            {
                _context.AspNetTag.Remove(tags.FirstOrDefault(t => t.Id == tagId));
                _context.SaveChanges();
                tags = _context.AspNetTag.ToList();
                StatusMessage = "Тег успешно удален!";
                return Page();
            }
            StatusMessage = "Ошибка при удалении тега!";
            return Page();
        }
    }
}
