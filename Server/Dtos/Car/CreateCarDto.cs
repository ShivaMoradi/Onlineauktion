namespace Server.Dtos.Car
{
    public class CreateCarDto
    {
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Year { get; set; }
        public string Color { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int Mileage { get; set; }
        public string EngineType { get; set; } = string.Empty;
        public string EngineDisplacement { get; set; } = string.Empty;
        public string Transmission { get; set; } = string.Empty;
        public string Features { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
    }
}
