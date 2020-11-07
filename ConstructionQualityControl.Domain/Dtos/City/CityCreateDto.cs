namespace ConstructionQualityControl.Domain.Dtos
{
    public class CityCreateDto
    {
        public string Name { get; set; }
        public int CoordX { get; set; }
        public int CoordY { get; set; }
        public RegionReadDto Region { get; set; }
    }
}
