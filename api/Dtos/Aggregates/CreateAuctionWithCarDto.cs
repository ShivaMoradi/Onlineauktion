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
        public CreateAuctionDto Auction { get; set; }
        public CreateCarDto Car { get; set; }
    }
}