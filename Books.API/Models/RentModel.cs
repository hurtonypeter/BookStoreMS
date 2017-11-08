using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.API.Models
{
    public class RentModel
    {
        public string Id { get; set; }
        
        public string Barcode { get; set; }

        public DateTime Start { get; set; }

        public DateTime? End { get; set; }

        public DateTime? ReturnDate { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }
    }
}
