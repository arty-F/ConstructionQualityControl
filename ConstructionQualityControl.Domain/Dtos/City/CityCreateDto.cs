namespace ConstructionQualityControl.Domain.Dtos
{
    public class CityCreateDto
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public RegionReadDto Region { get; set; }
    }
}
