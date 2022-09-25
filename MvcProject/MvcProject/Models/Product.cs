using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcProject.Models
{
    public class Product
    {
        [Key]
          public int Pid { get; set; }
       [ForeignKey("User")]
        public int Users_Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Gender { get; set; }
        [Required(ErrorMessage ="Enter valid price")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]

        public int Price { get; set; }
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]

        public int Discount { get; set; }
        [Required(ErrorMessage = "Required")]

        public int Rating { get; set; }
        public DateTime Publish { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        public virtual User User { get; set; }




    }
}