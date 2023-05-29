using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WellCarePharmacyWebapi.Models.Entities;

namespace WellCarePharmacyWebapi.Business_Logic_Layer.DTO
{
    public class OrdersRequest
    {
       

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        [ForeignKey("User")]
         public int UsersId { get; set; }


        [Required] 
         public IList<ProductOrderRequest> Products { get; set; }
      

    }
     
    public class ProductOrderRequest
    {
         [ForeignKey("Product")]
         public int ProductId { get; set; }
    
     }
}
