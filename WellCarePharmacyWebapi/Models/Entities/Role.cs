using System.ComponentModel.DataAnnotations;
using WellCarePharmacyWebapi.Models.Entities.Base;

namespace WellCarePharmacyWebapi.Models.Entities
{
    public class Role:BaseEntity
    {
        [Required]
        [MaxLength(20)]
        public string RoleName { get; set; }

    
    }
}
