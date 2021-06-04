using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantRater_Web.Models
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Enter at least 2 characters for this field")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public double Rating { get; set; }
    }
}