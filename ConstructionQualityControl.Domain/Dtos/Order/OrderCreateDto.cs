using System.Collections.Generic;

namespace ConstructionQualityControl.Domain.Dtos
{
    public class OrderCreateDto
    {
        public UserReadDto User { get; set; }
        public decimal PrePaid { get; set; }
        public decimal PostPaid { get; set; }
        public string Demands { get; set; }
        public bool IsRoot { get; set; }
        public List<OrderCreateDto> SubOrders { get; set; }
    }
}
