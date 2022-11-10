using CrudConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudConsoleApp.Db
{
    class KunskapsProvDbContext : DbContext
    {



        private const string connectionStr = "Server=localhost;Database=kunskaps_prov;User Id=sa;Password=P@ssw0rd;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionStr);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");


            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .IsRequired();

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasMaxLength(255);

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasMaxLength(255);


                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(255);

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(255);


                entity.Property(e => e.UserRegisteredAt)
                    .HasColumnName("user_registered_at")
                    .ValueGeneratedOnAdd();
                    
            });
          
        }

  

        public DbSet<User> Users { get; set; }

    }
}
