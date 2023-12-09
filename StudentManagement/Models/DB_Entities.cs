using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace StudentManagement.Models
{
    public class DB_Entities : DbContext
    {
        public DB_Entities() : base("StudentManagement") { }
        public DbSet<User> Users { get; set; }
        public DbSet<Sinhvien> Sinhviens { get; set; }
        public DbSet<Giangvien> Giangviens { get; set; }
        public DbSet<Class> Classes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<demoEntities>(null);
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Sinhvien>()
            .ToTable("Sinhviens");
            modelBuilder.Entity<Giangvien>()
           .ToTable("Giangviens");
            modelBuilder.Entity<Class>()
           .ToTable("Classes");

            modelBuilder.Entity<User>().HasOptional(u => u.sinhvien)
            .WithRequired(s => s.user)
            .Map(m => m.MapKey("userId"));

            modelBuilder.Entity<User>().HasOptional(u => u.giangvien)
             .WithRequired(s => s.user)
             .Map(m => m.MapKey("userId"));

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}