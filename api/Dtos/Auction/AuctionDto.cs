using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Bid;
using api.Dtos.Car;

namespace api.Dtos.Auction
{
    public class AuctionDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public long HighestBid { get; set; }
        public int? CarId { get; set; }
        public int? UserId { get; set; }
        public bool Status { get; set; } = false;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;

    }
}