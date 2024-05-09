using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Bid;
using Server.Models;

namespace api.Mappers
{
    public static class BidMappers
    {
        public static BidDto ToBidDto(this Bid bidModel)
        {
            return new BidDto
            {
                Id = bidModel.Id,
                BidAmount = bidModel.BidAmount,
                CreatedOn = bidModel.CreatedOn,
                AuctionId = bidModel.AuctionId
            };
        }


        public static Bid ToBidFromCreate(this CreateBidDDto bidDto, int auctionId)
        {
            return new Bid
            {
                BidAmount = bidDto.BidAmount,
                AuctionId = auctionId
            };
        }

    }
}