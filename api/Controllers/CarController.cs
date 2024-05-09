using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Car;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;

namespace api.Controllers
{   
    [Route("api/car")]
    [ApiController]
    public class CarController : ControllerBase
    {
     
        private readonly ICarRepository _carRepo;
        public CarController(ICarRepository carRepo)
        {   
            _carRepo = carRepo;
    
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cars = await _carRepo.GetAllAsync();
            var carDtos = cars.Select(s => s.ToCarDto());
            return Ok(carDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var car = await _carRepo.GetByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }  
            return Ok(car.ToCarDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCarDto carDto)
        {
            var carModel =  carDto.ToCarFromCreateDto();
            await _carRepo.CreateAsync(carModel);
            return CreatedAtAction(nameof(GetById), new {id = carModel.Id}, carModel.ToCarDto());
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCarRequestDto updateCarDto)
        {
            var carModel = await _carRepo.UpdateAsync(id, updateCarDto);
            if(carModel == null)
            {
                return NotFound();
            }
            return Ok(carModel.ToCarDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var carModel = await _carRepo.DeleteAsync(id);
            if(carModel == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}