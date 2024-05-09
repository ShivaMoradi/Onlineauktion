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





        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var auction = await _auctionRepo.GetByIdAsync(id);

            if (auction == null)
            {
                return NotFound();
            }
            return Ok(auction.ToAuctionDto());
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAuctionWithCarDto request)
        {
            var auctionModel = request.Auction.ToAuctionFromCreateDto();
            var carModel = request.Car.ToCarFromCreateDto();

            var createdAuction = await _auctionRepo.CreateAuctionAsync(auctionModel, carModel);

            return CreatedAtAction(nameof(GetById), new { id = createdAuction}, createdAuction.ToAuctionDto());
        }

    }
}