using System;

namespace ConstructionQualityControl.Data.Models
{
    public class Comment : IEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public virtual User User { get; set; }
        public virtual Order Order { get; set; }
        public string Text { get; set; }
    }
}

