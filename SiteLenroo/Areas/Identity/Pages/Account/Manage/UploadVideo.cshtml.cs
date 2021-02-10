using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SiteLenroo.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "admin, moderator")]
    public class UploadVideoModel : PageModel
    {
        public void OnGet()
        {
        }
        
        
        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            public string Path { get; set; }
            [Required(ErrorMessage = "Напишите описание")]
            public string Title { get; set; }
        }

        SiteLenrooContext2 _context = new SiteLenrooContext2();

        public async Task<IActionResult> OnPostAddedPhotoAsync(string url)
        {
            if (!String.IsNullOrEmpty(url))
            {
                AspNetPhoto video = new AspNetPhoto()
                {
                    Photo = url,
                    Title = Input.Title,
                    Video = true,
                    DateAdd = DateTime.Now
                };
                _context.AspNetPhoto.Add(video);
                _context.SaveChanges();
                StatusMessage = "Видео успешно добавлено!";
                return RedirectToPage("./Photo");
            }

            StatusMessage = "Ошибка. Видео не добавлено!";
            return Page();
        }
    }
}
