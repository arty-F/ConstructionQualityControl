namespace ConstructionQualityControl.Domain.Dtos
{
    public class ReportCreateDto
    {
        public UserReadDto User { get; set; }
        public OrderReadDto Order { get; set; }
        public string FileExtension { get; set; }
        public string Data { get; set; }
    }
}
