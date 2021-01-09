using System;

namespace ConstructionQualityControl.Data.Models
{
    public class WorkOffer
    {
        public int Id { get; set; }
        public virtual User Worker { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
    }
}
