using System;
using System.Collections.Generic;

namespace ConstructionQualityControl.Domain.Dtos
{
    public class WorkReadDto
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal PrePaid { get; set; }
        public decimal PostPaid { get; set; }
        public UserReadDto User { get; set; }
        public string Demands { get; set; }
        public bool IsRoot { get; set; }
        public CityReadDto City { get; set; }

        public List<WorkReadDto> SubOrders { get; set; } = new List<WorkReadDto>();
    }
}
