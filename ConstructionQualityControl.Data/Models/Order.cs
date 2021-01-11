using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstructionQualityControl.Data.Models
{
    public class Order : IEntity
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PrePaid { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PostPaid { get; set; }
        public virtual User User { get; set; }
        public string Demands { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsStarted { get; set; }
        public bool IsRoot { get; set; }
        public virtual City City { get; set; }

        public virtual List<Comment> Comments { get; set; } = new List<Comment>();
        public virtual List<Report> Reports { get; set; } = new List<Report>();
        public virtual List<Order> SubOrders { get; set; } = new List<Order>();
        public virtual List<WorkOffer> WorkOffers { get; set; } = new List<WorkOffer>();
    }
}
