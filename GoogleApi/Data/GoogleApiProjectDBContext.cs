using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using GoogleApi.Models;

namespace GoogleApi.Data
{
    public class GoogleApiProjectDBContext : DbContext
    {
        public GoogleApiProjectDBContext()
        {
        }

        public GoogleApiProjectDBContext(DbContextOptions<GoogleApiProjectDBContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Attribute> Attributes { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeAttribute> EmployeeAttributes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Attribute>(entity =>
            {
                entity.Property(e => e.AttrId).ValueGeneratedNever();    //   do i need this  with Guid?  can EFCore produce correct ids based on Guid?   
                                                                         
                entity.HasMany<EmployeeAttribute>(a => a.EmployeeAttributes)
                      .WithOne(ea => ea.EmpattrAttribute)
                      .HasForeignKey(a => a.EmpattrAttributeId)
                      .OnDelete(DeleteBehavior.Cascade)                                         //  eprepe na to energopoisw prwta ap tin basi!
                      .HasConstraintName("FK__EmployeeA__EMPAT__2A4B4B5E");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.EmpId).ValueGeneratedNever();
            });

            modelBuilder.Entity<EmployeeAttribute>(entity =>
            {
                entity.HasKey(ea => new { ea.EmpattrEmployeeId, ea.EmpattrAttributeId });     //  configure the composite key

                entity.HasOne<Employee>(ea => ea.EmpattrEmployee)         //  also need this because foreign keys (properties) dont folow the convention
                      .WithMany(e => e.EmployeeAttributes)
                      .HasForeignKey(ea => ea.EmpattrEmployeeId);

                entity.HasOne<Models.Attribute>(ea => ea.EmpattrAttribute)    // from both sides of the joining table
                      .WithMany(a => a.EmployeeAttributes)
                      .HasForeignKey(ea => ea.EmpattrAttributeId);
                      


                //entity.HasOne(d => d.EmpattrAttribute)         
                //    .WithMany(p => p.EmployeeAttributes)
                //    .HasForeignKey(d => d.EmpattrAttributeId)
                //    .OnDelete(DeleteBehavior.ClientSetNull);

                //entity.HasOne(d => d.EmpattrEmployee)
                //    .WithMany(p => p.EmployeeAttributes)
                //    .HasForeignKey(d => d.EmpattrEmployeeId)
                //    .OnDelete(DeleteBehavior.ClientSetNull);
            });

        }
    }
}
