using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimViec.Models
{
    public class applications
    {
        public int  Id { get; set; }
		[ForeignKey("Job")]

		public int JobID {  get; set; }
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
