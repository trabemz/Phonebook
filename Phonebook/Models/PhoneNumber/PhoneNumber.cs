namespace Phonebook.Models.PhoneNumber
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class PhoneNumber
    {
        public int ID { get; set; }

        [ForeignKey("District")]
        [Display(Name = "Округ")]
        public int? DistrictId { get; set; }

        [ForeignKey("Microdistrict")]
        [Display(Name = "Район")]
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
        [RegularExpression(
            @"^(495|499)\d{7}$",
            ErrorMessage = "Введите правильный стационарный номер г.Москва. Код Москвы (495 или 499).")]
        public string Number { get; set; }

        [Display(Name = "Примечание")]
        [StringLength(500, ErrorMessage = "Максимальная длина примечания 500 символов.")]
        public string Note { get; set; }
    }
}
