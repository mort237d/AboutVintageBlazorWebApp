using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AboutVintageAPI.Model;

namespace AboutVintageAPI.Data
{
    public class AboutVintageAPIContext : DbContext
    {
        public AboutVintageAPIContext (DbContextOptions<AboutVintageAPIContext> options)
            : base(options)
        {
        }

        public DbSet<AboutVintageAPI.Model.CustomerCount> CustomerCount { get; set; }
    }
}
