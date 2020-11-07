using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConstructionQualityControl.Data.Models
{
    public class Region : IEntity
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public virtual List<City> Cities { get; set; } = new List<City>();
    }
}
