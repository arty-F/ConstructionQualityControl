using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstructionQualityControl.Data.Models
{
    public class Order : IEntity
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual User User { get; set; }
        public string Demands { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsRoot { get; set; }

        public virtual List<Comment> Comments { get; set; } = new List<Comment>();
        public virtual List<Report> Reports { get; set; } = new List<Report>();
        public virtual List<Order> SubOrders { get; set; } = new List<Order>();
    }
}
