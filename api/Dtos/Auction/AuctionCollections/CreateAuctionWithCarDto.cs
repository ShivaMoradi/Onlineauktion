using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Auction;
using api.Dtos.Car;

namespace api.Dtos.Aggregates
{
    public class CreateAuctionWithCarDto
    {
        public required CreateAuctionDto Auction { get; set; }
        public required CreateCarDto Car { get; set; } // Car is currently not optional when creating an Auction.
    }
}