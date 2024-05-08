using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Car;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;

namespace api.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;
        public CarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Car> CreateAsync(Car carModel)
        {
            await _context.Cars.AddAsync(carModel);
            await _context.SaveChangesAsync();
            return carModel;
        }

        public async Task<Car?> DeleteAsync(int id)
        {
            var carModel = await _context.Cars.FirstOrDefaultAsync(x => x.Id == id);

            if(carModel == null)
            {
                return null;
            }

            _context.Cars.Remove(carModel);
            await _context.SaveChangesAsync();
            return carModel;
        }

        public async Task<List<Car>> GetAllAsync()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<Car?> GetByIdAsync(int id)
        {
            return await _context.Cars.FindAsync(id);
        }

        public async Task<Car?> UpdateAsync(int id, UpdateCarRequestDto carDto)
        {
            var existingCar = await _context.Cars.FirstOrDefaultAsync(x => x.Id == id);

            if(existingCar == null)
            {
                return null;
            }

            existingCar.Brand = carDto.Brand;
            existingCar.Model = carDto.Model;
            existingCar.Price = carDto.Price;
            existingCar.Year = carDto.Year;
            existingCar.ImageUrl = carDto.ImageUrl;
            existingCar.Mileage = carDto.Mileage;
            existingCar.EngineType = carDto.EngineType;
            existingCar.EngineDisplacement = carDto.EngineDisplacement;
            existingCar.Transmission = carDto.Transmission;
            existingCar.Features = carDto.Features;


            await _context.SaveChangesAsync();
            return existingCar;
        }
    }
}