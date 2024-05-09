using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Bid;

namespace api.Dtos.Auction
{
    public class AuctionWithBidsDto
    {

        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public long HighestBid { get; set; }
        public int? CarId { get; set; }
        public bool Status { get; set; } = false;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public List<BidDto> Bids { get; set; } = new List<BidDto>();

    }
}