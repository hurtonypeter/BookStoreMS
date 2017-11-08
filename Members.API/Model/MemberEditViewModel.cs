using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KonyvtarMVC.Web.Models.Member
{
    public class MemberEditViewModel
    {
        public string Id { get; set; }

        public int CardNumber { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public string BirthPlace { get; set; }

        public string Address { get; set; }

        public string MothersName { get; set; }
    }
}
