using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IntivePatronageKD.Entities
{
    public class UsersDbContext : DbContext
    {
        private string _connectionString = "Server=DESKTOP-SDDC5QS\\MSSQLSERVER01;Database=UsersDb;Trusted_Connection=True;";
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* ------------------------ */
            /*   USER TABLE PROPERTIES  */
            /* ------------------------ */

            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(50);            
            
            modelBuilder.Entity<User>()
                .Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(u => u.DateOfBirth)
                .IsRequired()
                .HasPrecision(0);

            modelBuilder.Entity<User>()
                .Property(u => u.Gender)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Weight)
                .HasPrecision(5, 2);

            modelBuilder.Entity<User>()
                .Property(u => u.AddressId)
                .IsRequired();
            /* ------------------------ */


            /* ------------------------ */
            /* ADDRESS TABLE PROPERTIES */
            /* ------------------------ */
            modelBuilder.Entity<Address>()
                .Property(a => a.Country)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Address>()
                .Property(a => a.City)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Address>()
                .Property(a => a.PostCode)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<Address>()
                .Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Address>()
                .Property(a => a.HouseNumber)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<Address>()
                .Property(a => a.LocalNumber)
                .HasMaxLength(10);
            /* ------------------------ */
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString); 
        }
    }
}
