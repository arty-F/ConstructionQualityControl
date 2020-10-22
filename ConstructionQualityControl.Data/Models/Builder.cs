using System;
using System.Collections.Generic;

namespace ConstructionQualityControl.Data.Models
{
    public class Builder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime RegistrationDate { get; set; }
        public City City { get; set; }

        public virtual List<Order> Orders { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Report> Reports { get; set; }
    }
}
