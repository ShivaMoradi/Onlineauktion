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


        // Get all bids
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bids = await _bidRepo.GetAllAsync();
            var bidDtos = bids.Select(s => s.ToBidDto());
            return Ok(bidDtos);
        }

        // Get 1 bid
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
           var bidModel = await _bidRepo.GetByIdAsync(id);
           if(bidModel == null)
           {
            return NotFound();
           }
           return Ok(bidModel.ToBidDto());
        }

        // Create bid
        [HttpPost("{auctionId:int}")]
        public async Task<IActionResult> Create([FromRoute] int auctionId, CreateBidDDto createBid)
        {
            if(!await _auctionRepo.AuctionExist(auctionId))
            {
                return BadRequest("Auction does not exist");
            } 
            var bidModel = createBid.ToBidFromCreate(auctionId);
            await _bidRepo.CreateAsync(bidModel);

            return CreatedAtAction(nameof(GetById), new {id = bidModel.Id}, bidModel.ToBidDto());
        }

        //Delete bid
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var bidModel = await _bidRepo.DeleteAsync(id);
            if(bidModel == null)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}