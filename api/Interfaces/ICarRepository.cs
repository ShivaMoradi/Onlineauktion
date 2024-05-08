using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Car;
using Server.Models;

namespace api.Interfaces
{
    public interface ICarRepository
    {
        Task<List<Car>> GetAllAsync();
        Task<Car?> GetByIdAsync(int id);
        Task<Car> CreateAsync(Car carModel);
        Task<Car?> UpdateAsync(int id, UpdateCarRequestDto carDto);
        Task<Car?> DeleteAsync(int id);
    }
}