namespace ShopPrep.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CityViewModel
    {
        public int CountryId { get; set; }

        public int CityId { get; set; }

        [Required]
        [Display(Name = "City")]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}