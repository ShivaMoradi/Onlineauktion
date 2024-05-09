using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {
            
        }

        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }
        
        
        // Setting up FOREIGN KEYS in AuctionTable in DB:
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            // One-to-One: Auction to Car
            modelBuilder.Entity<Auction>()
                .HasOne(a => a.Car)
                .WithOne(c => c.Auction) 
                .HasForeignKey<Auction>(a => a.CarId) // Foreign key in Auction table
                .OnDelete(DeleteBehavior.Cascade); // Car is removed when Auction is deleted

            // One-to-Many: Auction to Bids
            modelBuilder.Entity<Auction>()
                .HasMany(a => a.Bids)
                .WithOne(b => b.Auction)
                .HasForeignKey(b => b.AuctionId)
                .OnDelete(DeleteBehavior.Cascade); // Bids are removed when Auction is deleted


            base.OnModelCreating(modelBuilder);
        }
    }
}