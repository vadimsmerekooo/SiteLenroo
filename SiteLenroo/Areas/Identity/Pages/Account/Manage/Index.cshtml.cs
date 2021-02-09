using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SiteLenroo.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "admin, moderator")]
    public partial class IndexModel : PageModel
    {
        [TempData]
        public string StatusMessage { get; set; }
        SiteLenrooContext2 _context = new SiteLenrooContext2();
        
        public async Task<IActionResult> OnGetDeleteNewsAsync(int newsId)
        {
            if (!_context.AspNetNews.Any(n => n.Id == newsId))
            {
                StatusMessage = "Ошибка. Новость не найдена в базе данных.";
                return Page();
            }
            AspNetNews news = _context.AspNetNews.FirstOrDefault(n => n.Id == newsId);
            try
            {
                _context.AspNetNews.Remove(news);
                await _context.SaveChangesAsync();
                StatusMessage = "Новость успешно удалена.";
            }
            catch
            {
                StatusMessage = "Ошибка при удалении новости.";
            }


            return Page();
        }
        public async Task<IActionResult> OnGetBlockNewsAsync(int newsId)
        {
            if (!_context.AspNetNews.Any(n => n.Id == newsId))
            {
                StatusMessage = "Ошибка. Новость не найдена в базе данных.";
                return Page();
            }
            AspNetNews news = _context.AspNetNews.FirstOrDefault(n => n.Id == newsId);
            try
            {
                _context.AspNetNews.FirstOrDefault(n => n.Id == newsId).Blocked = true;
                await _context.SaveChangesAsync();
                StatusMessage = "Новость успешно заблокирована.";
            }
            catch
            {
                StatusMessage = "Ошибка при блокировке новости.";
            }


            return Page();
        }
        public async Task<IActionResult> OnGetUnblockNewsAsync(int newsId)
        {
            if (!_context.AspNetNews.Any(n => n.Id == newsId))
            {
                StatusMessage = "Ошибка. Новость не найдена в базе данных.";
                return Page();
            }
            AspNetNews news = _context.AspNetNews.FirstOrDefault(n => n.Id == newsId);
            try
            {
                _context.AspNetNews.FirstOrDefault(n => n.Id == newsId).Blocked = false;
                await _context.SaveChangesAsync();
                StatusMessage = "Новость успешно разблокирована.";
            }
            catch
            {
                StatusMessage = "Ошибка при разблокировке новости.";
            }


            return Page();
        }

    }
}
