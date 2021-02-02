using System;
using System.Collections.Generic;

namespace SiteLenroo.Models
{
    public partial class AspNetTag
    {
        public AspNetTag()
        {
            AspNetNews = new HashSet<AspNetNews>();
        }

        public int Id { get; set; }
        public string Tag { get; set; }

        public ICollection<AspNetNews> AspNetNews { get; set; }
    }
}
