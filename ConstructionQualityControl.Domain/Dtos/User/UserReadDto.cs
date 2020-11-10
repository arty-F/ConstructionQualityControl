using System;

namespace ConstructionQualityControl.Domain.Dtos
{
    public class UserReadDto
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string CompanyName { get; set; }
        public string CompanyDescription { get; set; }
        public DateTime BirthDate { get; set; }
        public CityReadDto City { get; set; }
        public string Role { get; set; }
    }
}
