using System;
using System.Collections.Generic;

namespace SiteLenroo.Models
{
    public partial class AspNetCategory
    {
        public AspNetCategory()
        {
            AspNetNews = new HashSet<AspNetNews>();
        }

        public int Id { get; set; }
        public string Category { get; set; }

        public ICollection<AspNetNews> AspNetNews { get; set; }
    }
}
