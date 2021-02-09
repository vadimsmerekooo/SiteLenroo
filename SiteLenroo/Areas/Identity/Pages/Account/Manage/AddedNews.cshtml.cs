using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SiteLenroo.Areas.Identity.Pages.Account.Manage
{
    public class AddedNewsModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }

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
            [Required(ErrorMessage = "Заполните поле: Наполнение")]
            public string Description { get; set; }
            public bool MainNews { get; set; }
            public int? Tag { get; set; }
            public int? Category { get; set; }
        }


        public void OnGet()
        {
        }
    }
}
