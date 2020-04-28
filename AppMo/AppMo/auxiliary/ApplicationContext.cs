using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace AppMo
{
    public class ApplicationContext : DbContext
    {
        public DbSet<actor> actors { get; set; }
        public DbSet<film> movies { get; set; }
        public DbSet<tag> tags { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
            Database.SetCommandTimeout(880055535);
        }

        public void ReCreate()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public void Where() => Database.EnsureCreated();

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<actor>().HasKey(actor => actor.code);
        //    modelBuilder.Entity<film>().HasKey(film => new { film.id, film.code });
        //    modelBuilder.Entity<tag>().HasKey(tag => new { tag.id, tag.name });
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=thisOne;Trusted_Connection=True;");
        }
    }
}
