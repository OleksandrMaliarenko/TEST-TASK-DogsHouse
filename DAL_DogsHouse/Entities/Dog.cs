using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace DAL_DogsHouse.Entities
{
    public class Dog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Color { get; set; }
        [Range(0, 200)]
        public double Tail_Length { get; set; }
        [Range(0, 200)]
        public double Weight { get; set; }
    }
}
