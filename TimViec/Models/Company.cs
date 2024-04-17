using System.ComponentModel.DataAnnotations;

namespace TimViec.Models
{
    public class Company
    {
        public  int Id { get; set; }

        [Required, StringLength(100)]
        public string Name_company { get; set; }

        [Required, StringLength(300)]
        public string Location { get; set; }

        [Required, StringLength(300)]
        public string Description { get; set; }

        [Required, StringLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Image { get; set; }

        [StringLength(50)]
        public string Company_size { get; set; }

        [StringLength(50)]
        public string Company_type { get; set; }

        public DateTime? Date { get; set; }

        public int? Id_city { get; set; }

        public City? city { get; set; }
    }
}
