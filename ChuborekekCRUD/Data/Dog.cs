using ChuborekekCRUD.Enum;
using System.ComponentModel.DataAnnotations;

namespace ChuborekekCRUD.Data
{
    public class Dog
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "Name Required!")]
        public string Name { get; set; }
        public DogBreed? dogBreed { get; set; }
    }
}
