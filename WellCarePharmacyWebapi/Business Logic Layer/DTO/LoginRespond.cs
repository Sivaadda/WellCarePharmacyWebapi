using WellCarePharmacyWebapi.Models.Entities;

namespace WellCarePharmacyWebapi.Business_Logic_Layer.DTO
{
    public class LoginRespond
    {
        public Users Users { get; set; }

        public string Token {  get; set; }
    }
}
