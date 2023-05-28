using System.ComponentModel.DataAnnotations;


namespace WellCarePharmacyWebapi.Business_Logic_Layer.DTO
{
    public class RegistrationRequest
    {

          [Required]
          [MaxLength(50)]
          public string Name { get; set; }

          [Required]
          [EmailAddress(ErrorMessage = "Enter emailaddress")]
          public string Email { get; set; }

          [Required]
          [DataType(DataType.Password)]
          [MinLength(8, ErrorMessage = "The Password must be at least 8 characters long.")]
          public string Password { get; set; }


          [Phone (ErrorMessage ="Enter valid phone number.")]
          public string PhoneNumber { get; set; }

    }
}
