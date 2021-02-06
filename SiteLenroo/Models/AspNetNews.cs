using SiteLenroo.Models;
using System;
using System.Collections.Generic;

namespace SiteLenroo
{
    public partial class AspNetNews
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int PreviewPhoto { get; set; }
        public string PreviewText { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int? Watching { get; set; }
        public bool Blocked { get; set; }
        public bool MainNews { get; set; }
        public int? Tag { get; set; }
        public int? Category { get; set; }

        public AspNetCategory CategoryNavigation { get; set; }
        public AspNetPhoto PreviewPhotoNavigation { get; set; }
        public AspNetTag TagNavigation { get; set; }
    }
}
