using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SiteLenroo.Areas.Identity.Pages.Account.Manage
{
    public class PhotoModel : PageModel
    {
        SiteLenrooContext2 _context = new SiteLenrooContext2();
        public List<AspNetPhoto> photo;
        [TempData]
        public string StatusMessage { get; set; }

        public PhotoModel()
        {
            photo = _context.AspNetPhoto.ToList();
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnGetDeletePhotoAsync(int id)
        {
            if(_context.AspNetPhoto.Any(p => p.Id == id))
            {
                _context.AspNetPhoto.Remove(_context.AspNetPhoto.First(p => p.Id == id));
                _context.SaveChanges();
                StatusMessage = "����������� ������� �������!";
                return Page();
            }
            StatusMessage = "������. ����������� �� �������, ���� �� �������!";
            return Page();
        }
    }
}
