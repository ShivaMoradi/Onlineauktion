using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{   
    [Table("Cars")]
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Price { get; set; }
        public int Year { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int Mileage { get; set; }
        public string EngineType { get; set; } = string.Empty;
        public string EngineDisplacement { get; set; } = string.Empty;
        public string Transmission { get; set; } = string.Empty;
        public List<string> Features { get; set; } = new();

        
        // navigation property - will not be generated in the db:
        public Auction Auction { get; set; } 
        
    }
}