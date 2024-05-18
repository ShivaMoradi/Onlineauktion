using Onlineauction.Models;
using Server.Dtos.Car;

namespace Server.Mappers
{
    public static class CarMapper
    {
        public static CarDto ToCarDto(this Car carModel)
        {
            return new CarDto
            {
                Id = carModel.Id,
                Brand = carModel.Brand,
                Model = carModel.Model,
                Price = carModel.Price,
                Year = carModel.Year,
                Color = carModel.Color,
                ImageUrl = carModel.ImageUrl,
                Mileage = carModel.Mileage,
                EngineType = carModel.EngineType,
                EngineDisplacement = carModel.EngineDisplacement,
                Transmission = carModel.Transmission,
                Features = carModel.Features
            };
        }


        public static Car ToCarFromCreateDto(this CreateCarDto carDto)
        {
            return new Car
            {
                Brand = carDto.Brand,
                Model = carDto.Model,
                Price = carDto.Price,
                Year = carDto.Year,
                Color = carDto.Color,
                ImageUrl = carDto.ImageUrl,
                Mileage = carDto.Mileage,
                EngineType = carDto.EngineType,
                EngineDisplacement = carDto.EngineDisplacement,
                Transmission = carDto.Transmission,
                Features = carDto.Features
            };
        }

    }
}
