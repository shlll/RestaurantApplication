using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restaurant.Models
{
    public class Type
    {
        public int Id { get; set; }
        [Required]
        public string RestaurantType { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Comments { get; set; }
        [Required]
        public int Ratings { get; set; }
        public virtual Name Name { get; set; }
        public int NameId{ get; set; }
        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }
    }
}