using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SiteLenroo.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "admin, moderator")]
    public class AddedNewsModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }

        SiteLenrooContext2 _context = new SiteLenrooContext2();

        public class InputModel
        {
            [Required(ErrorMessage = "Заполните поле: Заголовок")]
            public string Title { get; set; }
            [Required(ErrorMessage = "Заполните поле: Ссылка")]
            public string Url { get; set; }
            [Required(ErrorMessage = "Выберите изображение")]
            public int PreviewPhoto { get; set; }
            [Required(ErrorMessage = "Заполните поле: Текст для превью")]
            public string PreviewText { get; set; }
            public string Description { get; set; }
            public bool MainNews { get; set; }
            public int? Tag { get; set; }
            public int? Category { get; set; }
        }

        public async Task<IActionResult> OnPostCreateAsync(string editor, string photo, string tag, string cat, bool mainnews)
        {
            if (ModelState.IsValid)
            {
                AspNetNews aspNetNews = new AspNetNews()
                {
                    Title = Input.Title.Trim(),
                    Url = Input.Url.Trim(),
                    PreviewPhoto = _context.AspNetPhoto.Any(p => p.Title == photo) ? _context.AspNetPhoto.FirstOrDefault(p => p.Title == photo).Id : 0,
                    PreviewText = Input.PreviewText,
                    Description = editor,
                    Tag = _context.AspNetTag.Any(t => t.Tag == tag) ? _context.AspNetTag.FirstOrDefault(t => t.Tag == tag).Id : 0,
                    Category = _context.AspNetCategory.Any(c => c.Category == cat) ? _context.AspNetCategory.FirstOrDefault(c => c.Category == cat).Id : 0,
                    MainNews = mainnews,
                    Blocked = false,
                    Date = DateTime.Now,
                    Watching = 0
                };
                if(aspNetNews.PreviewPhoto == 0 || String.IsNullOrEmpty(aspNetNews.Description) || aspNetNews.Tag == 0 || aspNetNews.Category == 0)
                {
                    StatusMessage = "Ошибка. Один из параметров не валиден!";
                    return Page();
                }
                _context.AspNetNews.Add(aspNetNews);
                _context.SaveChanges();
                StatusMessage = "Новость успешно добавлена!";
                return RedirectToPage("./Index");
            }
            StatusMessage = "Ошибка. Не все поля заполнены!";
            
            return Page();
        }



        public void OnGet()
        {
        }
    }
}
