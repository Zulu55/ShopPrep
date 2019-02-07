namespace ShopPrep.Web.Controllers.API
{
    using System;
    using Data;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[Controller]")]
    public class ProductsController : Controller
    {
        private readonly IRepository repository;
        private readonly ILogger<ProductsController> logger;

        public ProductsController(IRepository repository, ILogger<ProductsController> logger)
        {
            this.repository = repository;
            this.logger = logger;
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
    }
}