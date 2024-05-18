using Onlineauction.Models;
using Server.Dtos.Car;

namespace Server.Interfaces
{
    public interface ICarRepository
    {
        Task<bool> CarExists(int id);
        Task<List<Car>> GetAllCarsAsync();
        Task<Car?> GetCarByIdAsync(int id);
        Task<int> AddCarReturnIdAsync(Car car);
        Task DeleteCarAsync(int id);
        Task<Car?> UpdateCarAsync(int id, UpdateCarDto carDto);

    }
}
