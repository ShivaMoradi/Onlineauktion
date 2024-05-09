using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Auction;
using api.Dtos.Bid;
using api.Dtos.Car;
using Server.Models;

namespace api.Mappers
{
    public static class AuctionMapper
    {
        
        public static AuctionDto ToAuctionDto(this Auction auctionModel)
        {
            return new AuctionDto
            {
                Id = auctionModel.Id,
                Title = auctionModel.Title,
                HighestBid = auctionModel.HighestBid,
                CarId = auctionModel.CarId,
                Status = auctionModel.Status,
                StartTime = auctionModel.StartTime,
                EndTime = auctionModel.EndTime,
                CreatedOn = auctionModel.CreatedOn,

            };
        }


        public static Auction ToAuctionFromCreateDto(this CreateAuctionDto auctionDto)
        {
            return new Auction
            {
                Title = auctionDto.Title,
                HighestBid = auctionDto.HighestBid,
                StartTime = auctionDto.StartTime,
                EndTime = auctionDto.EndTime
            };
        }

    }
}