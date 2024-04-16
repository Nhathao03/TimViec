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

        [StringLength(50)]
        public string? R1_Language { get; set; }

        [StringLength(50)]
        public string? R2_Language { get; set; }

        [StringLength(50)]
        public string? R3_Language { get; set; }

        public int? cityId { get; set; }

        public City? city { get; set; }

        public List<Job>? Jobs { get; set; }

    }
}
