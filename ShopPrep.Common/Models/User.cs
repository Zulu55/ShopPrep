namespace ShopPrep.Common.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }
    }
}
