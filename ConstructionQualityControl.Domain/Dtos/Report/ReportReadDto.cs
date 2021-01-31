using System;

namespace ConstructionQualityControl.Domain.Dtos
{
    public class ReportReadDto
    {
        public DateTime CreationDate { get; set; }
        public string FileExtension { get; set; }
        public byte[] Data { get; set; }
    }
}
