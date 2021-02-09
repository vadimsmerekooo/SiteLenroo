using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteLenroo.Controllers
{
    [Authorize(Roles = "admin, moderator")]
    public class UpdateSelectController : Controller
    {
        public ActionResult UpdateTags()
        {
            string resultHtmlList = string.Empty;
            foreach (AspNetTag tagItem in new SiteLenrooContext2().AspNetTag)
            {
                resultHtmlList += $"<li class=\"select__item\">{tagItem.Tag}</li>";
            }
            return Content(resultHtmlList);
        }
        public ActionResult UpdateCategory()
        {
            string resultHtmlList = string.Empty;
            foreach (AspNetCategory catItem in new SiteLenrooContext2().AspNetCategory)
            {
                resultHtmlList += $"<li class=\"select__item\">{catItem.Category}</li>";
            }
            return Content(resultHtmlList);
        }
        public ActionResult UpdatePhoto()
        {
            string resultHtmlList = string.Empty;
            foreach (AspNetPhoto photoItem in new SiteLenrooContext2().AspNetPhoto)
            {
                resultHtmlList += $"<li class=\"select__item\"><div style=\"display: flex;\"><img src=\"{photoItem.Photo}\" height = \"50\" width = \"50\"/><h4 style = \"margin-left: 50px;\">{@photoItem.Title}</h4></div></li>";
            }
            return Content(resultHtmlList);
        }
    }
}
