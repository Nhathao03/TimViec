using System.ComponentModel.DataAnnotations;

namespace TimViec.Models
{
    public class applications
    {
        public int  Id { get; set; }
        public int Id_job {  get; set; }
        public Job? Job { get; set; }

        [Required, StringLength(200)]
        public string ImageCV { get; set; }
        public DateTime Create_at { get; set; }
        [StringLength(200)]
        public string Note { get; set; }
        [Required]
        public string Status { get; set; }

    }
}
