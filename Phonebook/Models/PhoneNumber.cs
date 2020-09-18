using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Models
{
    public class PhoneNumber
    {
        public int Id { get; set; }

        [ForeignKey("District")]
        [Display(Name = "Район")]
        public int? DistrictId { get; set; }

        [ForeignKey("Microdistrict")]
        [Display(Name = "Микрорайон")]
        public int? MicrodistrictId { get; set; }

        [Display(Name = "Адрес")]
        [StringLength(200, ErrorMessage = "Максимальная длина адреса 200 символов.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Необходимо ввести имя.")]
        [StringLength(100, ErrorMessage = "Максимальная длина имени 100 символов.")]
        [Display(Name = "ФИО")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Необходимо ввести номер телефона.")]
        [Display(Name = "Телефон")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^7(495|499)[0-9]{7}$", 
            ErrorMessage = "Введите правильный стационарный номер г.Москва. Код России (7), код Москвы (495 или 499).")]
        public string Number { get; set; }

        [Display(Name = "Примечание")]
        [StringLength(500, ErrorMessage = "Максимальная длина примечания 500 символов.")]
        public string Note { get; set; }

    }
}
