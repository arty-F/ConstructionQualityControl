using System;

namespace ConstructionQualityControl.Domain.Dtos
{
    public class ReportReadDto
    {
        public DateTime CreationDate { get; set; }
        public string FileExtension { get; set; }
        public string Data { get; set; }
    }
}
