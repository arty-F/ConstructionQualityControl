namespace ConstructionQualityControl.Domain.Dtos
{
    public class CityReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RegionReadDto Region { get; set; }
    }
}
