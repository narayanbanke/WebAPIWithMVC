using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InsuranceAPPv1
{
    public class ApllicationDbContext:DbContext
    {
        public ApllicationDbContext() { }   
        public ApllicationDbContext(DbContextOptions<ApllicationDbContext> options) : base(options) { }
      public DbSet<Models.user> users { get; set; }
    
    }
}
