using System.ComponentModel.DataAnnotations;

namespace WellCarePharmacyWebapi.Models.Entities.Base
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
