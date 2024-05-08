using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
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

            auctionModel.Car = carModel;
            auctionModel.CarId = carModel.Id;

            await _context.Auctions.AddAsync(auctionModel);
            await _context.SaveChangesAsync();

            return auctionModel;

        }

    }
}