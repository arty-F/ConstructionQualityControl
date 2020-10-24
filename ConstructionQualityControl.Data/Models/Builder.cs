using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstructionQualityControl.Data.Models
{
    public class Builder : IEntity
    {
        public int Id { get; set; }
        [Required, MaxLength(25)]
        public string Name { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public DateTime RegistrationDate { get; set; }
        public City City { get; set; }

        public virtual List<Order> Orders { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Report> Reports { get; set; }
    }
}
