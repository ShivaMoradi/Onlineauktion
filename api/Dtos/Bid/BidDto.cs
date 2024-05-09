using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Bid
{
    public class BidDto
    {
        public int Id { get; set; }
        public int BidAmount { get; set; } 
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int AuctionId { get; set; }

    }
}