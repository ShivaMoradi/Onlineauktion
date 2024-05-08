using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Bid
{
    public class CreateBidDto
    {
        public int BidAmount { get; set; } 
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int AuctionId { get; set; }
        public int UserId { get; set; }
    }
}