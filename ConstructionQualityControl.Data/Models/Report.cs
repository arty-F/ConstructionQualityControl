using System;

namespace ConstructionQualityControl.Data.Models
{
    public class Report : IEntity
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public Builder Builder { get; set; }
        public Order Order { get; set; }
        public string FileExtension { get; set; }
        public byte[] Data { get; set; }
    }
}
