using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConstructionQualityControl.Data.Models
{
    public class User : IEntity
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Login { get; set; }
        [Required, MinLength(6), MaxLength(30)]
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        [Required, MaxLength(25)]
        public string FirstName { get; set; }
        [Required, MaxLength(25)]
        public string LastName { get; set; }
        [MaxLength(25)]
        public string Patronymic { get; set; }
        [MaxLength(35)]
        public string CompanyName { get; set; }
        public string CompanyDescription { get; set; }
        public DateTime BirthDate { get; set; }
        public City City { get; set; }
        [MaxLength(10)]
        public string Role { get; set; }

        public virtual List<Order> Orders { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Report> Reports { get; set; }
    }
}
