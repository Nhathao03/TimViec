using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimViec.Models
{
    public class Job
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; }

        public int CompanyID { get; set; }
        public Company? Company { get; set; }

        [Required]
        public string Location { get; set; }
        [Required]
        public decimal Salary { get; set; }

        [Required]
        public string Description { get; set; }

        public int? Id_skill { get; set; }

        public Skill? Skill { get; set; }

        public int? Id_rank { get; set; }

        public Rank? Rank { get; set; }

        public string img {  get; set; }

        public string? R1_Language { get; set; }

        public string? R2_Language { get; set; }

        public string? R3_Language { get; set; }

		[ForeignKey("Type_work")]
		public int? Type_workID { get; set; }

        public Type_work? Type_work { get; set; }
    }
}
