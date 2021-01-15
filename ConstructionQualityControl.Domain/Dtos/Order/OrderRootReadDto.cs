using System;
using System.Collections.Generic;

namespace ConstructionQualityControl.Domain.Dtos
{
    public class OrderRootReadDto
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public UserReadDto User { get; set; }
        public string Demands { get; set; }
        public decimal PostPaid { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsStarted { get; set; }
        public CityReadDto City { get; set; }
        public List<WorkOfferReadDto> WorkOffers { get; set; }
    }
}
