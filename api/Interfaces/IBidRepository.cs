using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models;

namespace api.Interfaces
{
    public interface IBidRepository
    {
        Task<bool> BidExists(int id);
        Task<List<Bid>> GetAllAsync();
        Task<Bid?> GetByIdAsync(int id);
        Task<Bid> CreateAsync(Bid bidModel);
    }
}