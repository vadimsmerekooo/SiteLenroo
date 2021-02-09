using System;
using System.Collections.Generic;

namespace SiteLenroo
{
    public partial class AspNetPhoto
    {
        public AspNetPhoto()
        {
            AspNetNews = new HashSet<AspNetNews>();
        }

        public int Id { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }
        public DateTime DateAdd { get; set; }

        public ICollection<AspNetNews> AspNetNews { get; set; }
    }
}
