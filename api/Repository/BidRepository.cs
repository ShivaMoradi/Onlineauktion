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

        public async Task<Bid> CreateAsync(Bid bidModel)
        {
            await _context.Bids.AddAsync(bidModel);
            await _context.SaveChangesAsync();
            return bidModel;
        }

        public async Task<List<Bid>> GetAllAsync()
        {
            return await _context.Bids.ToListAsync();
        }

        #pragma warning disable CS8603
        public async Task<Bid> GetByIdAsync(int id)
        {
            return await _context.Bids.FindAsync(id);
        }
        #pragma warning restore CS8603

    }
}