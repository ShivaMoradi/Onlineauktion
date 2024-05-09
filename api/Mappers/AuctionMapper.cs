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


        public static AuctionWithBidsDto ToAuctionWithBidsDto(this Auction auctionModel)
        {
            return new AuctionWithBidsDto
            {
                Id = auctionModel.Id,
                Title = auctionModel.Title,
                HighestBid = auctionModel.HighestBid,
                CarId = auctionModel.CarId,
                Status = auctionModel.Status,
                StartTime = auctionModel.StartTime,
                EndTime = auctionModel.EndTime,
                CreatedOn = auctionModel.CreatedOn,
                Bids = auctionModel.Bids.Select(b => b.ToBidDto()).ToList()
            };
        }

        
        public static AuctionWithCarDto ToAuctionWithCarDto(this Auction auctionModel)
        {
            return new AuctionWithCarDto
            {
                Id = auctionModel.Id,
                Title = auctionModel.Title,
                HighestBid = auctionModel.HighestBid,
                CarId = auctionModel.CarId,
                Status = auctionModel.Status,
                StartTime = auctionModel.StartTime,
                EndTime = auctionModel.EndTime,
                CreatedOn = auctionModel.CreatedOn,
                Car = auctionModel.Car.ToCarDto()
            };
        }

        public static AuctionFullDto ToAuctionFullDto(this Auction auctionModel)
        {
            return new AuctionFullDto
            {
                Id = auctionModel.Id,
                Title = auctionModel.Title,
                HighestBid = auctionModel.HighestBid,
                CarId = auctionModel.CarId,
                Status = auctionModel.Status,
                StartTime = auctionModel.StartTime,
                EndTime = auctionModel.EndTime,
                CreatedOn = auctionModel.CreatedOn,
                Car = auctionModel.Car.ToCarDto(),
                Bids = auctionModel.Bids.Select(b => b.ToBidDto()).ToList() 
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


        public static Auction ToAuctionFromUpdate(this UpdateAuctionDto auctionDto)
        {
            return new Auction
            {
                Title = auctionDto.Title
            };
        }

    }
}