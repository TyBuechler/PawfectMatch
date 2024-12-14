using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Components.Pages.Models;

namespace PawfectMatch.Data
{
    public class PawfectMatchContext : DbContext
    {
        public PawfectMatchContext (DbContextOptions<PawfectMatchContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } // Example table
        public DbSet<PawfectMatch.Components.Pages.Models.User> User { get; set; } = default!;
    }
}
