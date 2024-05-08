using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Car;
using Server.Models;

namespace api.Mappers
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
              ImageUrl = carModel.ImageUrl,
              Mileage = carModel.Mileage,
              EngineType = carModel.EngineType,
              EngineDisplacement = carModel.EngineDisplacement,
              Transmission = carModel.Transmission,
              Features = carModel.Features
            };
        }
  


        public static Car ToCarFromCreateDto(this CreateCarRequestDto carModel)
        {
            return new Car
            {
                Brand = carModel.Brand,
                Model = carModel.Model,
                Price = carModel.Price,
                Year = carModel.Year,
                ImageUrl = carModel.ImageUrl,
                Mileage = carModel.Mileage,
                EngineType = carModel.EngineType,
                EngineDisplacement = carModel.EngineDisplacement,
                Transmission = carModel.Transmission,
                Features = carModel.Features
            };
        }

    }
}