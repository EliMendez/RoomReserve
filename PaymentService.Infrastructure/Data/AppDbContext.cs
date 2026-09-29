using Microsoft.EntityFrameworkCore;
using PaymentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Payment>(entity =>
            {

                entity.HasKey(b => b.PaymentId);

                entity.Property(b => b.BookingId)
                    .IsRequired();

                entity.Property(b => b.Amount)
                  .HasColumnType("decimal(18,2)");

                entity.Property(b => b.Email)
                    .IsRequired();

                entity.Property(b => b.Status)
                    .IsRequired();

                entity.Property(b => b.CreatedAt)
                    .IsRequired();
            });
        }
    }
}
