using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InsuranceAPI
{
    public class ApllicationDbContext:DbContext
    {
        public ApllicationDbContext() { }   
        public ApllicationDbContext(DbContextOptions<ApllicationDbContext> options) : base(options) { }
      public DbSet<Models.user> users { get; set; }
        public DbSet<Models.LossTypes> LossTypes { get; set; }
    }
}
