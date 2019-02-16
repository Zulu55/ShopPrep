namespace ShopPrep.Web.Controllers.API
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ShopPrep.Web.Data.Entities;
    using ShopPrep.Web.Helpers;

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[Controller]")]
    public class ProductsController : Controller
    {
        private readonly IRepository repository;
        private readonly ILogger<ProductsController> logger;
        private readonly IUserHelper userHelper;

        public ProductsController(
            IRepository repository, 
            ILogger<ProductsController> logger, 
            IUserHelper userHelper)
        {
            this.repository = repository;
            this.logger = logger;
            this.userHelper = userHelper;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            try
            {
                return this.Ok(this.repository.GetProducts());
            }
            catch (Exception ex)
            {
                var message = $"Error: {ex}";
                this.logger.LogError(message);
                return this.BadRequest(message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] Common.Models.Product product)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var user = await this.userHelper.GetUserByEmail(product.UserEmail);
            if (user == null)
            {
                return this.BadRequest("Invalid user");
            }

            var imageUrl = string.Empty;
            if (product.ImageArray != null && product.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(product.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "wwwroot\\images\\Products"; 
                var fullPath = $"~/images/Products/{file}";
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    imageUrl = fullPath;
                }
            }

            var entityProduct = new Product
            {
                IsAvailabe = product.IsAvailabe,
                LastPurchase = product.LastPurchase,
                LastSale = product.LastSale,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                User = user,
                ImageUrl = imageUrl
            };

            var newProduct = await this.repository.AddProductAsync(entityProduct);
            return Ok(newProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute] int id, [FromBody] Common.Models.Product product)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            var oldProduct = this.repository.GetProduct(id);
            if (oldProduct == null)
            {
                return this.BadRequest("Product Id don't exists.");
            }

            //TODO: Upload images
            oldProduct.IsAvailabe = product.IsAvailabe;
            oldProduct.LastPurchase = product.LastPurchase;
            oldProduct.LastSale = product.LastSale;
            oldProduct.Name = product.Name;
            oldProduct.Price = product.Price;
            oldProduct.Stock = product.Stock;

            await this.repository.UpdateProductAsync(oldProduct);
            return Ok(oldProduct);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var product = this.repository.GetProduct(id);
            if (product == null)
            {
                return this.NotFound();
            }

            await this.repository.RemoveProductAsync(product);
            return Ok(product);
        }
    }
}