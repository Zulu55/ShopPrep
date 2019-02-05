namespace ShopPrep.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using ShopPrep.Common.Models;
    using ShopPrep.Web.Models;

    public class OrdersController : Controller
    {
        private readonly IRepository repository;

        public OrdersController(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var model = await this.repository.GetOrdersAsync(this.User.Identity.Name);
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = await this.repository.GetDetailTempsAsync(this.User.Identity.Name);
            return this.View(model);
        }

        public IActionResult AddProduct()
        {
            var model = new OrderDetailViewModel
            {
                Quantity = 1,
                Products = this.GetProductsList()
            };

            return View(model);
        }

        private IEnumerable<SelectListItem> GetProductsList()
        {
            var products = this.repository.GetProducts().ToList();
            products.Insert(0, new Product
            {
                Id = 0,
                Name = "(Select a product...)"
            });

            return products.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            }).ToList();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddItemViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                await this.repository.AddItemToOrderAsync(model, this.User.Identity.Name);
                return this.RedirectToAction("Create");
            }

            return this.View(model);
        }

        public async Task<IActionResult> Increase(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await this.repository.ModifyOrderDetailTempQuantityAsync(id.Value, 1);
            return this.RedirectToAction("Create");
        }

        public async Task<IActionResult> Decrease(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await this.repository.ModifyOrderDetailTempQuantityAsync(id.Value, -1);
            return this.RedirectToAction("Create");
        }

        public async Task<IActionResult> DeleteItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await this.repository.DeleteDetailTempAsync(id.Value);
            return this.RedirectToAction("Create");
        }

        public async Task<IActionResult> ConfirmOrder()
        {
            var response = await this.repository.ConfirmOrderAsync(this.User.Identity.Name);
            if (response)
            {
                return this.RedirectToAction("Index");
            }

            return this.RedirectToAction("Create");
        }

        public async Task<IActionResult> Deliver(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await this.repository.GetOrdersAsync(id.Value);
            if (order == null)
            {
                return NotFound();
            }

            var model = new DeliverViewModel
            {
                Id = order.Id,
                DeliveryDate = DateTime.Today
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Deliver(DeliverViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                await this.repository.DeliverOrder(model);
                return this.RedirectToAction("Index");
            }

            return this.View();
        }

    }
}