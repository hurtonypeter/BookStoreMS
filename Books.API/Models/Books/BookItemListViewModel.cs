using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KonyvtarMVC.Web.Models.Books
{
    public class BookItemListViewModel
    {
        public string Id { get; set; }

        public string Barcode { get; set; }

        public string Condition { get; set; }

        public string State { get; set; }
    }
}
