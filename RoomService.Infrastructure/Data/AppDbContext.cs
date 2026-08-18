using Microsoft.EntityFrameworkCore;
using RoomService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Infrastructure.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomRate> RoomRates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.RoomId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Capacity)
                    .IsRequired();

                entity.Property(e => e.PricePerHour)
                  .HasColumnType("decimal(18,2)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<RoomRate>(entity =>
            {
                entity.HasKey(e => e.RoomRateId);

                entity.HasOne(e => e.Room)
                    .WithMany(e => e.RoomRates)
                    .HasForeignKey(e => e.RoomId);

                entity.Property(e => e.StartTime)
                    .IsRequired();

                entity.Property(e => e.EndTime)
                    .IsRequired();

                entity.Property(e => e.PricePerHour)
                  .HasColumnType("decimal(18,2)");
            });
        }
    }
}
