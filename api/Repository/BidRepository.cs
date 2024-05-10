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
    public class BidRepository : IBidRepository
    {
        private readonly ApplicationDbContext _context;
        public BidRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> BidExists(int id)
        {
            return await _context.Bids.AnyAsync(s => s.Id == id);
        }

        public async Task<Bid> CreateAsync(Bid bidModel)
        {
            await _context.Bids.AddAsync(bidModel);
            await _context.SaveChangesAsync();
            return bidModel;
        }

        public async Task<Bid?> DeleteAsync(int id)
        {
            var bidModel = await _context.Bids.FindAsync(id);
            if(bidModel == null)
            {
                return null;
            }
            _context.Bids.Remove(bidModel);
            await _context.SaveChangesAsync();

            return bidModel;
        }

        public async Task<List<Bid>> GetAllAsync()
        {
            return await _context.Bids.ToListAsync();
        }

        public async Task<Bid?> GetByIdAsync(int id)
        {
            return await _context.Bids.FindAsync(id);
 
        }
    }
}