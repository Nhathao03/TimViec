namespace TimViec.Models
{
    public class StatusJob
    {
        public int Id { get; set; } 
        public string Email { get; set; }    
        public string Status { get; set; }

        public string Fullname { get; set; }

        public string Note { get; set; }

        public ApplicationUser? User { get; set; }


    }
}
