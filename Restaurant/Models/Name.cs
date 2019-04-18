﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restaurant.Models
{
    public class Name
    {
        public int Id { get; set; }
        [Required]
        public string RestaurantName { get; set; }
        [Required]
        public string Information { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public virtual List<Type> Types { get; set; }
    }
}