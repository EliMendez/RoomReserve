using BookingService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Infrastructure.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(b => b.BookingId);

                entity.Property(b => b.BookingId)
                    .IsRequired();

                entity.Property(b => b.BookingDate)
                    .IsRequired();

                entity.Property(b => b.StartTime)
                    .IsRequired();

                entity.Property(b => b.EndTime)
                    .IsRequired();

                entity.Property(b => b.NumberOfAttendees)
                    .IsRequired();

                entity.Property(b => b.Duration)
                  .HasColumnType("decimal(18,2)");

                entity.Property(b => b.Subtotal)
                  .HasColumnType("decimal(18,2)");

                entity.Property(b => b.Total)
                  .HasColumnType("decimal(18,2)");

                entity.Property(b => b.Status)
                    .HasMaxLength(10)
                    .IsRequired();

                entity.Property(b => b.PaymentStatus)
                    .HasMaxLength(10)
                    .IsRequired();
            });
        }
    }
}
