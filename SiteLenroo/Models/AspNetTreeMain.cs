using System;
using System.Collections.Generic;

namespace SiteLenroo
{
    public partial class AspNetTreeMain
    {
        public AspNetTreeMain()
        {
            AspNetTree = new HashSet<AspNetTree>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

        public ICollection<AspNetTree> AspNetTree { get; set; }
    }
}
