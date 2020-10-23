using System;

namespace ConstructionQualityControl.Data.Models
{
    public class Comment : IEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Builder Builder { get; set; }
        public Customer Customer { get; set; }
        public Order Order { get; set; }
        public string Text { get; set; }
    }
}

