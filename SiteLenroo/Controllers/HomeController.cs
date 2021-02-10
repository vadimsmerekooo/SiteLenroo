using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SiteLenroo.Models;

namespace SiteLenroo.Controllers
{
    public class HomeController : Controller
    {
        SiteLenrooContext2 _context = new SiteLenrooContext2();

        public IActionResult Index()
        {
            List<AspNetPhoto> dontUsePhoto = new List<AspNetPhoto>();
            foreach (AspNetPhoto aspNetPhoto in _context.AspNetPhoto)
            {
                if (_context.AspNetNews.Count(n => n.PreviewPhoto == aspNetPhoto.Id) == 0)
                    dontUsePhoto.Add(aspNetPhoto);
            }
            return View(dontUsePhoto);
        }
    }
}
