using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WellCarePharmacyWebapi.Models.Entities;

namespace WellCarePharmacyWebapi.Business_Logic_Layer.DTO
{
    public class OrderRespond
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public int UsersId { get; set; }
        [ForeignKey("UsersId")]
        public virtual User Users { get; set; }


        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Products { get; set; }
    }
}
