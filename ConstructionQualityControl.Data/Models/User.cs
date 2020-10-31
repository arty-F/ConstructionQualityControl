using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstructionQualityControl.Data.Models
{
    public class User : IEntity
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Login { get; set; }
        [Required, MinLength(6), MaxLength(30)]
        public string Password { get; set; }
        [NotMapped]
        public DateTime RegistrationDate { get; set; }
        public bool isCustomer { get; set; }
        public Customer Customer { get; set; }
        public Builder Builder { get; set; }
    }
}
