using Books.API.DataModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.API.DataModel.Entities
{
    public class BookItem
    {
        public string Id { get; set; }

        public string Barcode { get; set; }

        public BookCondition Condition { get; set; }

        //TODO: ez egy notmapped property, ami a Rents-ből megmondja, hogy kölcsönzött/szabad/lejárt-e az állapota
        public string State { get; set; }

        public string BookId { get; set; }

        public virtual Book Book { get; set; }

        public virtual ICollection<Rent> Rents { get; set; } = new HashSet<Rent>();
    }
}
