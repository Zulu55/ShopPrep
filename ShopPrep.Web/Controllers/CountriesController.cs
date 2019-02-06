namespace ShopPrep.Web.Controllers
{
    using System.Threading.Tasks;
    using Common.Models;
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    public class CountriesController : Controller
    {
        private readonly IRepository repository;

        public CountriesController(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IActionResult> DeleteCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await this.repository.GetCityAsync(id.Value);
            if (city == null)
            {
                return NotFound();
            }

            var countryId = await this.repository.DeleteCityAsync(city);
            return this.RedirectToAction($"Details/{countryId}");
        }

        public async Task<IActionResult> EditCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await this.repository.GetCityAsync(id.Value);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        [HttpPost]
        public async Task<IActionResult> EditCity(City city)
        {
            if (this.ModelState.IsValid)
            {
                var countryId = await this.repository.UpdateCity(city);
                if (countryId != 0)
                {
                    return this.RedirectToAction($"Details/{countryId}");
                }
            }

            return this.View(city);
        }

        public async Task<IActionResult> AddCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await this.repository.GetCountryAsync(id.Value);
            if (country == null)
            {
                return NotFound();
            }

            var model = new CityViewModel { CountryId = country.Id };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCity(CityViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                await this.repository.AddCity(model);
                return this.RedirectToAction($"Details/{model.CountryId}");
            }

            return this.View(model);
        }

        public async Task<IActionResult> Index()
        {
            return View(await this.repository.GetCountriesAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await this.repository.GetCountryAsync(id.Value);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Country country)
        {
            if (ModelState.IsValid)
            {
                await this.repository.AddCountryAsync(country);
                return RedirectToAction(nameof(Index));
            }

            return View(country);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await this.repository.GetCountryAsync(id.Value);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Country country)
        {
            if (ModelState.IsValid)
            {
                await this.repository.UpdateCountryAsync(country);
                return RedirectToAction(nameof(Index));
            }

            return View(country);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await this.repository.GetCountryAsync(id.Value);
            if (country == null)
            {
                return NotFound();
            }

            await this.repository.RemoveCountryAsync(country);
            return RedirectToAction(nameof(Index));
        }
    }
}