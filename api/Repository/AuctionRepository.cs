using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Mappers;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;

namespace api.Repository
{
    public class AuctionRepository : IAuctionRepository
    {
        private readonly ApplicationDbContext _context;
        public AuctionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<bool> AuctionExist(int id)
        {
            return _context.Auctions.AnyAsync(a => a.Id == id);
        }

        public async Task<Auction?> GetByIdAsync(int id)
        {
            return await _context.Auctions
            .Include(x => x.Bids)
            .Include(x => x.Car)
            .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Auction>> GetAllAsync()
        {
            return await _context.Auctions
            .Include(x => x.Bids)
            .Include(x => x.Car)
            .ToListAsync();
        }

        public async Task<Auction> CreateAuctionAsync(Auction auctionModel, Car carModel)
        {
            await _context.Cars.AddAsync(carModel);
            await _context.SaveChangesAsync();
            
            auctionModel.CarId = carModel.Id;
            auctionModel.Car = carModel;

           
            await _context.Auctions.AddAsync(auctionModel);
            await _context.SaveChangesAsync();

            return auctionModel;
        }

        public async Task<Auction?> UpdateAsync(int id, Auction auctionModel)
        {
            var existingAuction = await _context.Auctions.FindAsync(id);

            if(existingAuction == null)
            {
                return null;
            }

            // Only edits the Title of an auction.
            existingAuction.Title = auctionModel.Title;
    
            await _context.SaveChangesAsync();
            return existingAuction;           
        }
        
        // Delete auction, car and bids
        public async Task<Auction?> DeleteAsync(int id)
        {
            var auctionModel = await _context.Auctions
                .Include(x => x.Bids)
                .Include(x => x.Car)
                .FirstOrDefaultAsync(a => a.Id == id);
                
            if(auctionModel == null)
            {
                return null;
            }

            if (auctionModel.Car != null)
            {
                _context.Cars.Remove(auctionModel.Car);
            }
                    

            _context.Auctions.Remove(auctionModel);
            await _context.SaveChangesAsync();
            
            return auctionModel;
        }
    }
}