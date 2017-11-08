using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.API.DataModel.Entities
{
    public class Rent
    {
        public string Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime? End { get; set; }

        public DateTime? ReturnDate { get; set; }

        public string MemberId { get; set; }
        
        public string BookItemId { get; set; }

        public virtual BookItem BookItem { get; set; }
    }
}
