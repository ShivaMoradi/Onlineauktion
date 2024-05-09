using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Car
{
    public class CreateCarDto
    {
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Price { get; set; }
        public int Year { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int Mileage { get; set; }
        public string EngineType { get; set; } = string.Empty;
        public string EngineDisplacement { get; set; } = string.Empty;
        public string Transmission { get; set; } = string.Empty;
        public List<string>? Features { get; set; } 
    }
}