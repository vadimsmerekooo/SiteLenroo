using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SiteLenroo.Areas.Identity.Pages.Account.Manage
{
    public static class ManageNavPages
    {
        public static string Index => "Index"; 
        public static string ChangeBanner => "ChangeBanner";
        public static string AddedNews => "AddedNews";
        public static string Photo => "Photo";
        public static string UploadPhoto => "UploadPhoto";
        public static string UploadVideo => "UploadVideo";
        public static string Tags => "Tags";
        public static string AddedTag => "AddedTag";
        public static string Categorys => "Categorys";
        public static string AddedCategory => "AddedCategory";
        public static string TreeMenu => "TreeMenu";
        public static string Roles => "Roles";

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);
        public static string ChangeBannerNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangeBanner);
        public static string AddedNewsNavClass(ViewContext viewContext) => PageNavClass(viewContext, AddedNews);
        public static string PhotoNavClass(ViewContext viewContext) => PageNavClass(viewContext, Photo);
        public static string UploadPhotoNavClass(ViewContext viewContext) => PageNavClass(viewContext, UploadPhoto);
        public static string UploadVideoNavClass(ViewContext viewContext) => PageNavClass(viewContext, UploadVideo);
        public static string TagsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Tags);
        public static string AddTagsNavClass(ViewContext viewContext) => PageNavClass(viewContext, AddedTag);
        public static string CategoryesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Categorys);
        public static string AddCategoryesNavClass(ViewContext viewContext) => PageNavClass(viewContext, AddedCategory);
        public static string RolesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Roles);
        public static string TreeMenuNavClass(ViewContext viewContext) => PageNavClass(viewContext, TreeMenu);

        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
