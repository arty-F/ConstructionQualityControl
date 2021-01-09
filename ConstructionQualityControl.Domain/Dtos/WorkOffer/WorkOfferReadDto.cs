using System;

namespace ConstructionQualityControl.Domain.Dtos
{
    public class WorkOfferReadDto
    {
        public int Id { get; set; }
        public UserReadDto Worker { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
    }
}
