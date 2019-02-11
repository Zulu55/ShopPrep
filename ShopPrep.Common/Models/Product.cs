namespace ShopPrep.Common.Models
{
    using System;

    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public DateTime? LastPurchase { get; set; }

        public DateTime? LastSale { get; set; }

        public bool IsAvailabe { get; set; }

        public double Stock { get; set; }

        public string UserEmail { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.ImageUrl))
                {
                    return null;
                }

                return $"https://shopprep.azurewebsites.net{this.ImageUrl.Substring(1)}";
            }
        }
    }
}