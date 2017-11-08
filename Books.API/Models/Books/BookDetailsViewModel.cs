using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KonyvtarMVC.Web.Models.Books
{
    public class BookDetailsViewModel
    {
        public string Id { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public string Isbn { get; set; }

        public List<BookItemListViewModel> BookItems { get; set; } = new List<BookItemListViewModel>();
    }
}
