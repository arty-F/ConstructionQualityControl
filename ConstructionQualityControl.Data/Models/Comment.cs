using System;

namespace ConstructionQualityControl.Data.Models
{
    public class Comment : IEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }
        public Order Order { get; set; }
        public string Text { get; set; }
    }
}

