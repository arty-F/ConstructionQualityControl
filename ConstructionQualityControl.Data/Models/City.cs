using System.ComponentModel.DataAnnotations;

namespace ConstructionQualityControl.Data.Models
{
    public class City : IEntity
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public virtual Region Region { get; set; }
        [Required]
        public int CoordX { get; set; }
        [Required]
        public int CoordY { get; set; }
    }
}
