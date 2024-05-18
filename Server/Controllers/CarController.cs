using Microsoft.AspNetCore.Mvc;
using Onlineauction.Models;
using Server.Dtos.Car;
using Server.Interfaces;
using Server.Mappers;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarRepository _carRepository;

        public CarController(ICarRepository carRepository)
        {
            _carRepository = carRepository;        
        }

        [HttpPost]
        public async Task<IActionResult> AddCar([FromBody] CreateCarDto carDto)
        {
            var carModel = carDto.ToCarFromCreateDto();

            int id = await _carRepository.AddCarReturnIdAsync(carModel);
            carModel.Id = id;

            return CreatedAtAction(nameof(GetCarById), new { id = carModel.Id }, carModel.ToCarDto());
        }
        



       [HttpGet]
       public async Task<IActionResult> GetAllCars()
        {
            var carModels = await _carRepository.GetAllCarsAsync();

            if(carModels == null)
            {
                return NoContent();
            }
            return Ok(carModels.Select(c => c.ToCarDto()));
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCarById(int id)
        {
            var carModel = await _carRepository.GetCarByIdAsync(id);

            if(carModel == null)
            {
                return NotFound();
            }

            return Ok(carModel.ToCarDto());
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            if (!await _carRepository.CarExists(id))
            {
                return NotFound();
            }

            await _carRepository.DeleteCarAsync(id);

            return NoContent();
        }


        [HttpPatch]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateCar(int id, [FromBody] UpdateCarDto updateCarDto)
        {      
            var updatedCar = await _carRepository.UpdateCarAsync(id, updateCarDto);

            if(updatedCar == null)
            {
                return NotFound();
            }

            return Ok(updatedCar);
        }

    }

}
