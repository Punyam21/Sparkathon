﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcProject.Models
{
    public class Cart
    {

        [Key]
        public int cid { get; set; }
        [ForeignKey("Product")]
        public int Products_Pid { get; set; }
         
        public int Users_Id { get; set; }

        public int Quantity { get; set; }

        public int TotalPrice { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }


    }
}