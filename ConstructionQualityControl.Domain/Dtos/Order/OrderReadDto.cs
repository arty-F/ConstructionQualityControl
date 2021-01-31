using System;
using System.Collections.Generic;

namespace ConstructionQualityControl.Domain.Dtos
{
    public class OrderReadDto
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal PrePaid { get; set; }
        public decimal PostPaid { get; set; }
        public UserReadDto User { get; set; }
        public string Demands { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsStarted { get; set; }
        public bool IsRoot { get; set; }
        public CityReadDto City { get; set; }

        public List<OrderReadDto> SubOrders { get; set; }
        public List<CommentReadDto> Comments { get; set; }
        public List<ReportReadDto> Reports { get; set; }
    }
}
