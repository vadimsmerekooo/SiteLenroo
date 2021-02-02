using System;
using System.Collections.Generic;

namespace SiteLenroo
{
    public partial class AspNetTree
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int ParentMain { get; set; }
        public int Parent { get; set; }

        public AspNetTreeMain ParentMainNavigation { get; set; }
    }
}
