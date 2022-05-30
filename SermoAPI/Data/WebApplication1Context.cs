using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sermo_WAPI_Trial2;
using SermoApp2.Models;

namespace WebApplication1.Data
{
    public class WebApplication1Context : DbContext
    {
        public WebApplication1Context()
        {
        }

        public WebApplication1Context (DbContextOptions<WebApplication1Context> options)
            : base(options)
        {
        }

        public DbSet<Sermo_WAPI_Trial2.User>? User { get; set; }

        public DbSet<Sermo_WAPI_Trial2.Log>? Log { get; set; }

        public DbSet<Sermo_WAPI_Trial2.Message>? Message { get; set; }

        public DbSet<Sermo_WAPI_Trial2.Ratings>? Ratings { get; set; }
    }
}
