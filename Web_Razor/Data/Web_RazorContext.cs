using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web_Razor.Models;

namespace Web_Razor.Data
{
    public class Web_RazorContext : DbContext
    {
        public Web_RazorContext (DbContextOptions<Web_RazorContext> options)
            : base(options)
        {
        }

        public DbSet<Web_Razor.Models.Movie> Movie { get; set; }
    }
}
