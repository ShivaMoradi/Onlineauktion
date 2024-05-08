using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Bid;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace api.Controllers
{   
    [Route("api/bid")]
    [ApiController]
    public class BidController : ControllerBase
    { 
        private readonly IBidRepository _bidRepo;
        private readonly IAuctionRepository _auctionRepo;
        public BidController(IBidRepository bidRepo, IAuctionRepository auctionRepo)
        {
            _bidRepo = bidRepo;
            _auctionRepo = auctionRepo;
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bids = await _bidRepo.GetAllAsync();
            var bidDtos = bids.Select(s => s.ToBidDto());
            return Ok(bidDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
           var bidModel = await _bidRepo.GetByIdAsync(id);

           if(bidModel == null)
           {
            return NotFound();
           }

           return Ok(bidModel.ToBidDto());
        }





        [HttpPost("{auctionId}")]
        public async Task<IActionResult> Create([FromRoute] int auctionId, CreateBidDto bidDto)
        {
            if(!await _auctionRepo.AuctionExist(auctionId))
            {
                return BadRequest("Auction does not exist");
            }

            var bidModel = bidDto.ToBidFromCreate(auctionId);
            await _bidRepo.CreateAsync(bidModel);

            return CreatedAtAction(nameof(GetById), new {id = bidModel}, bidModel.ToBidDto());
        }

    }
}