using System;

namespace ConstructionQualityControl.Domain.Dtos
{
    public class UserCreateDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
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
