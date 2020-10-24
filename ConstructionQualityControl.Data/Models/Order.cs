using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstructionQualityControl.Data.Models
{
    public class Order : IEntity
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public Customer Customer { get; set; }
        public Builder Builder { get; set; }
        public string Demands { get; set; }
        [NotMapped]
        public bool IsCompleted { get; set; }
        [NotMapped]
        public bool IsActive { get; set; }

        public virtual List<Comment> Comments { get; set; }
        public virtual List<Report> Reports { get; set; }
        public virtual List<Order> SubOrders { get; set; }
    }
}
