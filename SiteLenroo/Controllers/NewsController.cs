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
        public IActionResult Index() => View(_context.AspNetNews.OrderBy(d => d.Date).ToList());
        public IActionResult NewsDetails(string newsUrl) => View(_context.AspNetNews.FirstOrDefault(n => n.Url == newsUrl));
    }
}
