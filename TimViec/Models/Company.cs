﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimViec.Models
{
    public class Company
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name_company { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Description { get; set; }

        [Required, StringLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Image { get; set; }

        [StringLength(50)]
        public int? Company_size { get; set; }

        [StringLength(50)]
        public string Company_type { get; set; }

        public DateTime? Date { get; set; }
		[ForeignKey("City")]

		public int? CityID { get; set; }

        public City? City { get; set; }
    }
}
