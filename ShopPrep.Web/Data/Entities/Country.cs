namespace ShopPrep.Web.Data.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Country
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Country")]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; }

        [Display(Name = "# Cities")]
        public int NumberCities { get { return this.Cities == null ? 0 : this.Cities.Count; } }
    }
}