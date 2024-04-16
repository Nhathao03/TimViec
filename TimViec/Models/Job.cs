using System.ComponentModel.DataAnnotations;

namespace TimViec.Models
{
    public class Job
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; }

        public int CompanyId { get; set; }
        public Company? Company { get; set; }

        [Required, StringLength(100)]
        public string Location { get; set; }
        [Required]
        public decimal Salary { get; set; }

        [Required, StringLength(300)]
        public string  Description { get; set; }

        public int? SkillId { get; set; }

        public Skill? Skill { get; set; }

        public int? RankId { get; set; }

        public Rank? Rank { get; set; }

        public string img {  get; set; }

        public string? R1_Language { get; set; }

        public string? R2_Language { get; set; }

        public string? R3_Language { get; set; }
        public int? Type_workId { get; set; }

        public Type_work? Type_work { get; set; }

        public List<application>? applications { get; set; }

    }
}
