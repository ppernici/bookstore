using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.WebUI.Models
{
    public class NavigationViewModel
    {
        public IEnumerable<string> Categories { get; set; }
        public IEnumerable<string> Authors { get; set; }
        public string CurrentCategory { get; set; }
        public string CurrentAuthor { get; set; }
    }
}