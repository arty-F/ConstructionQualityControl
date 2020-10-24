using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstructionQualityControl.Data.Models
{
    public class Customer : IEntity
    {
        public int Id { get; set; }
        [Required, MaxLength(25)]
        public string FirstName { get; set; }
        [Required, MaxLength(25)]
        public string LastName { get; set; }
        [MaxLength(25)]
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        [NotMapped]
        public DateTime RegistrationDate { get; set; }
        public City City { get; set; }

        public virtual List<Order> Orders { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
