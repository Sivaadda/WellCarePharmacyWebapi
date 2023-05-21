using System.ComponentModel.DataAnnotations;

namespace WellCarePharmacyWebapi.Business_Logic_Layer.DTO
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress(ErrorMessage = "Enter emailaddress")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "The Password must be at least 8 characters long.")]
        public string Password { get; set; }
    }
}
