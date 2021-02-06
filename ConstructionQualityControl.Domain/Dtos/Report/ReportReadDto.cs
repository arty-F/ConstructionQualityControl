using System;

namespace ConstructionQualityControl.Domain.Dtos
{
    public class ReportReadDto
    {
        public UserReadDto User { get; set; }
        public DateTime CreationDate { get; set; }
        public string Data { get; set; }
    }
}
