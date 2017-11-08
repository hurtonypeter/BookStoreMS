using Members.API.DataModel.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Members.API.DataModel
{
    public class MemberContext : DbContext
    {
        public DbSet<Member> Members { get; set; }

        public MemberContext(DbContextOptions<MemberContext> options)
            : base(options)
        {
        }
    }
}
