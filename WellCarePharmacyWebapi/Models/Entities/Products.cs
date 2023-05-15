﻿using System.ComponentModel.DataAnnotations;
using WellCarePharmacyWebapi.Models.Entities.Base;

namespace WellCarePharmacyWebapi.Models.Entities
{
    public class Products:BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string ProductName { get; set; }

        [Required]
       
        public float Price { get; set; }

        [MaxLength(100)]
        public string Descripition { get; set; }

        
        public decimal Discount { get; set; }

        [Required]
        [MaxLength(30)]
        public string Status { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }


       
    }
}
