namespace ShopPrep.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common.Models;
    using Microsoft.AspNetCore.Http;

    public class ProductViewModel : Product
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}