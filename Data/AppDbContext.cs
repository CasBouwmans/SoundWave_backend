using Microsoft.EntityFrameworkCore;
using Domain;
using System.Collections.Generic;

namespace App.Data
{

    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<Review> Reviews { get; set; }
    }
}
