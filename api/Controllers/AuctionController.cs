using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Aggregates;
using api.Dtos.Auction;
using api.Dtos.Car;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{   
    [Route("api/auction")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionRepository _auctionRepo;

        public AuctionController(IAuctionRepository auctionRepo, ICarRepository carRepo)
        {
            _auctionRepo = auctionRepo;
        }

        // Get all auctions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var auctions = await _auctionRepo.GetAllAsync();
            if(auctions == null)
            {
                return NotFound();
            }
            var auctionDtos = auctions.Select(s => s.ToAuctionDto());
            return Ok(auctionDtos);
        }

        // Get all auctions with car and bids
        [HttpGet("full")]
        public async Task<IActionResult> GetAllDetails()
        {
            var auctions = await _auctionRepo.GetAllAsync();
            var auctionDtos = auctions.Select(a => a.ToAuctionFullDto());

            if (auctions == null)
            {
                return NotFound();
            }
            return Ok(auctionDtos);
        }

        // Get all auctions with bids
        [HttpGet("bids")]
        public async Task<IActionResult> GetAllWithBids()
        {
            var auctions = await _auctionRepo.GetAllAsync();
            var auctionDtos = auctions.Select(a => a.ToAuctionWithBidsDto());

            if (auctions == null)
            {
                return NotFound();
            }
            return Ok(auctionDtos);
        }

        // Get all Auctions with car
        [HttpGet("car")]
        public async Task<IActionResult> GetAllWithCar()
        {
            var auctions = await _auctionRepo.GetAllAsync();
            var auctionDtos = auctions.Select(a => a.ToAuctionWithCarDto());

            if (auctions == null)
            {
                return NotFound();
            }
            return Ok(auctionDtos);
        }


        // Get by ID
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var auction = await _auctionRepo.GetByIdAsync(id);

            if (auction == null)
            {
                return NotFound();
            }
            return Ok(auction.ToAuctionDto());
        }

        // Create Auction with car
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAuctionWithCarDto request)
        {
            var auctionModel = request.Auction.ToAuctionFromCreateDto();
            var carModel = request.Car.ToCarFromCreateDto();

            var createdAuction = await _auctionRepo.CreateAuctionAsync(auctionModel, carModel);

            return CreatedAtAction(nameof(GetById), new { id = createdAuction.Id }, createdAuction.ToAuctionDto());
            
        }


        // Edit auction Title
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateAuctionDto updateDtoRequest)
        {
            var auction = await _auctionRepo.UpdateAsync(id, updateDtoRequest.ToAuctionFromUpdate());
            if(auction == null)
            {
                return NotFound("Auction not found");
            }
            return Ok(auction.ToAuctionDto());
        }


        // Delete auction with car and bids
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var auctionModel = await _auctionRepo.DeleteAsync(id);
            if(auctionModel == null)
            {
                return NotFound("Auction does not exist");
            }

            return NoContent();
        }
    }
}