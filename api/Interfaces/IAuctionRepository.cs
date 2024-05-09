using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models;

namespace api.Interfaces
{
    public interface IAuctionRepository
    {
        Task<bool> AuctionExist(int id);
        Task<List<Auction>> GetAllAsync(); 
        Task<Auction?> GetByIdAsync(int id);
        Task<Auction> CreateAuctionAsync(Auction auctionModel, Car carModel);
        Task<Auction?> UpdateAsync(int id, Auction auctionModel);
        Task<Auction?> DeleteAsync(int id);
    
    }
}