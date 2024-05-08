using System.ComponentModel.DataAnnotations.Schema;

namespace TimViec.Models
{
    public class StatusJob
    {
        public int ID { get; set; } 
        public string Email { get; set; }
        public int Status { get; set; }
        public string Fullname { get; set; }
        public string Note { get; set; }
        public string? imgCV { get; set; }

		[ForeignKey("Job")]
        public int JobID { get; set; }
        public Job? Job { get; set; }
	}
}
