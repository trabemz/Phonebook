using System.ComponentModel.DataAnnotations;

namespace Phonebook.Models.Location
{
    public abstract class TerritorialUnit
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Необходимо ввести название.")]
        [StringLength(50, ErrorMessage = "Максимальная длина 50 символов.")]
        [Display(Name = "Название")]
        public string Name { get; set; }
    }
}
