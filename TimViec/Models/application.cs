using System.ComponentModel.DataAnnotations;

namespace TimViec.Models
{
    public class application
    {
        public int  Id { get; set; }
        

        [Required, StringLength(200)]
        public string ImageCV { get; set; }
        public DateTime Create_at { get; set; }
        [StringLength(200)]
        public string Note { get; set; }
        [Required]
        public string Status { get; set; }

        public int JobId { get; set; }
        public Job? Job { get; set; }

    }
}
