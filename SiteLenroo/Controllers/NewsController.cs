using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteLenroo.Controllers
{
    public class NewsController : Controller
    {
        SiteLenrooContext2 _context = new SiteLenrooContext2();
        public IActionResult Index(int page = 1)
        {
            int pageSize = 6;
            List<AspNetNews> aspNetNews = _context.AspNetNews.OrderByDescending(d => d.Date).ToList();
            var count = aspNetNews.Count();
            var items = aspNetNews.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel indexViewModel = new IndexViewModel()
            {
                PageViewModel = pageViewModel,
                News = items
            };
            return View(indexViewModel);
        }
        public IActionResult NewsDetails(string newsUrl) => View(_context.AspNetNews.FirstOrDefault(n => n.Url == newsUrl));
    }
}
