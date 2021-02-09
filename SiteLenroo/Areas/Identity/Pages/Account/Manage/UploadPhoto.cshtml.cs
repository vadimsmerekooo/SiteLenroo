using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SiteLenroo.Areas.Identity.Pages.Account.Manage
{
    public class UploadPhotoModel : PageModel
    {

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

        public UploadPhotoModel(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        SiteLenrooContext2 _context = new SiteLenrooContext2();
        IWebHostEnvironment _appEnvironment;

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAddedPhotoAsync(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                if (!uploadedFile.ContentType.Contains("image"))
                {
                    StatusMessage = "Ошибка. Выбранный файл, не изображение.";
                    return Page();
                }

                string path = "/upload/photo/" + uploadedFile.FileName;

                if (System.IO.File.Exists(_appEnvironment.WebRootPath + path))
                    path = "/upload/photo/" + new Random().Next(999999999) + uploadedFile.FileName;

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                AspNetPhoto photo = new AspNetPhoto()
                {
                    Photo = path,
                    Title = Input.Title,
                    DateAdd = DateTime.Now
                };
                _context.AspNetPhoto.Add(photo);
                _context.SaveChanges();
                StatusMessage = "Фото успешно загружено!";
                return RedirectToPage("./Photo");
            }
            return Page();
        }

    }
}
