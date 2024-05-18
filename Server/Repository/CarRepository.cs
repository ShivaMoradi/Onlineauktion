using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Onlineauction.Models;
using Server.Data;
using Server.Dtos.Car;
using Server.Interfaces;
using System.Data;
using System.Runtime.InteropServices;

namespace Server.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;
        public CarRepository(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<int> AddCarReturnIdAsync(Car carModel)
        {
            string query = @"
                INSERT INTO cars (brand, model, price, year, color, imageUrl, mileage, engine_type, engine_displacement, transmission, features)
                VALUES (@brand, @model, @price, @year, @color, @imageUrl, @mileage, @engine_type, @engine_displacement, @transmission, @features);
                SELECT LAST_INSERT_ID();
            ";

            using (var command = new MySqlCommand(query, _context.Connection))
            {
                command.Parameters.AddWithValue("@brand", carModel.Brand);
                command.Parameters.AddWithValue("@model", carModel.Model);
                command.Parameters.AddWithValue("@price", carModel.Price);
                command.Parameters.AddWithValue("@year", carModel.Year);
                command.Parameters.AddWithValue("@color", carModel.Color);
                command.Parameters.AddWithValue("@imageUrl", carModel.ImageUrl);
                command.Parameters.AddWithValue("@mileage", carModel.Mileage);
                command.Parameters.AddWithValue("@engine_type", carModel.EngineType);
                command.Parameters.AddWithValue("@engine_displacement", carModel.EngineDisplacement);
                command.Parameters.AddWithValue("@transmission", carModel.Transmission);
                command.Parameters.AddWithValue("@features", carModel.Features);
                return Convert.ToInt32(await command.ExecuteScalarAsync());
            }

        }

        public async Task<bool> CarExists(int id)
        {
            string query = "SELECT COUNT(1) FROM cars WHERE id = @id";

            using (var command = new MySqlCommand(query, _context.Connection))
            {
                command.Parameters.AddWithValue("@id", id);
                var count = Convert.ToInt32(await command.ExecuteScalarAsync());
                return count > 0;
            }
        }

        public async Task DeleteCarAsync(int id)
        {
            string query = "DELETE FROM cars WHERE id = @id";

            using (var command = new MySqlCommand(query, _context.Connection))
            {
                command.Parameters.AddWithValue("@id", id);
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            var cars = new List<Car>();
            string query = "SELECT * FROM cars";

            using (var command = new MySqlCommand(query, _context.Connection))
            {
                using (var reader = await command.ExecuteReaderAsync()) 
                { 
                
                    while (await reader.ReadAsync()) 
                    {
                        cars.Add(new Car
                        {
                            Id = reader.GetInt32("id"),
                            Brand = reader.GetString("brand"),
                            Model = reader.GetString("model"),
                            Price = reader.GetDouble("price"),
                            Year = reader.GetInt32("year"),
                            Color = reader.GetString("color"),
                            ImageUrl = reader.GetString("imageUrl"),
                            Mileage = reader.GetInt32("mileage"),
                            EngineType = reader.GetString("engine_type"),
                            EngineDisplacement = reader.GetString("engine_displacement"),
                            Transmission = reader.GetString("transmission"),
                            Features = reader.GetString("features")
                        });

                    }
                }
            }
            return cars;
        }

        public async Task<Car?> GetCarByIdAsync(int id)
        {
            string query = "SELECT * FROM cars WHERE id = @id";

            using (var command = new MySqlCommand(query, _context.Connection))
            {
                command.Parameters.AddWithValue("@id", id);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        var car = new Car
                        {
                            Id = reader.GetInt32("id"),
                            Brand = reader.GetString("brand"),
                            Model = reader.GetString("model"),
                            Price = reader.GetDouble("price"),
                            Year = reader.GetInt32("year"),
                            Color = reader.GetString("color"),
                            ImageUrl = reader.GetString("imageUrl"),
                            Mileage = reader.GetInt32("mileage"),
                            EngineType = reader.GetString("engine_type"),
                            EngineDisplacement = reader.GetString("engine_displacement"),
                            Transmission = reader.GetString("transmission"),
                            Features = reader.GetString("features")
                        };
                        return car;
                    }
                }   
            }
            return null;
        }


        public async Task<Car?> UpdateCarAsync(int id, UpdateCarDto carDto)
        {
            string query = @"
            UPDATE cars 
            SET brand = @brand, model = @model, price = @price, year = @year, color = @color, imageUrl = @imageUrl, 
                mileage = @mileage, engine_type = @engine_type, engine_displacement = @engine_displacement, 
                transmission = @transmission, features = @features
            WHERE id = @id";

            using (var command = new MySqlCommand(query, _context.Connection))
            {

                command.Parameters.AddWithValue("@brand", carDto.Brand);
                command.Parameters.AddWithValue("@model", carDto.Model);
                command.Parameters.AddWithValue("@price", carDto.Price);
                command.Parameters.AddWithValue("@year", carDto.Year);
                command.Parameters.AddWithValue("@color", carDto.Color);
                command.Parameters.AddWithValue("@imageUrl", carDto.ImageUrl);
                command.Parameters.AddWithValue("@mileage", carDto.Mileage);
                command.Parameters.AddWithValue("@engine_type", carDto.EngineType);
                command.Parameters.AddWithValue("@engine_displacement", carDto.EngineDisplacement);
                command.Parameters.AddWithValue("@transmission", carDto.Transmission);
                command.Parameters.AddWithValue("@features", carDto.Features);

                command.Parameters.AddWithValue("@id", id);
                await command.ExecuteNonQueryAsync();
            }
            return await GetCarByIdAsync(id);
        }
    }
}
