using Books.API.DataModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KonyvtarMVC.Web.Models.Books
{
    public class BookItemEditViewModel
    {
        public string Id { get; set; }

        public string BookId { get; set; }

        public string Barcode { get; set; }

        public BookCondition Condition { get; set; }
    }
}
