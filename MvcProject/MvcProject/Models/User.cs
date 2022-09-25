using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcProject.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Required")]
        public int Mobileno { get; set; }

        public string ImageUrl { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.EmailAddress)]
        public string Emailid { get; set; }
        [Required(ErrorMessage = "Required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "length between 4 than 50 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage ="Check and fill")]
        public string Usertype { get; set; }
        public string Address { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}