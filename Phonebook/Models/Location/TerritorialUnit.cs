using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
