using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Car;

namespace api.Dtos.Auction
{
    public class CreateAuctionDto
    {
        public string Title { get; set; } = string.Empty;
        public long HighestBid { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}