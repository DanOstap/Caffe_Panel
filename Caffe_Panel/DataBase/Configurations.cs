using Caffe_Panel.Models;
using Microsoft.EntityFrameworkCore;

namespace Caffe_Panel.DataBase
{
    public class Configurations: DbContext
    {
     public  DbSet<User> User { get; set; }
     public DbSet<Items> Items { get; set; }

        public Configurations(DbContextOptions<Configurations> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasKey("Id");
            modelBuilder.Entity<Items>().HasKey("Id");

        }
    }
}
