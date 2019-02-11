namespace ShopPrep.Web.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models;
    using ShopPrep.Web.Data.Entities;

    public interface IRepository
    {
        Task AddCity(CityViewModel model);

        Task AddCountryAsync(Country country);

        Task AddItemToOrderAsync(AddItemViewModel model, string userName);

        Task<Product> AddProductAsync(Product product);

        Task<bool> ConfirmOrderAsync(string userName);

        Task<int> DeleteCityAsync(City city);

        Task DeleteDetailTempAsync(int id);

        Task DeliverOrder(DeliverViewModel model);

        Task<City> GetCityAsync(int id);

        IEnumerable<SelectListItem> GetComboProducts();


        IEnumerable<SelectListItem> GetComboCountries();

        IEnumerable<SelectListItem> GetComboCities(int conuntryId);

        Task<Country> GetCountryAsync(City city);

        Task<IEnumerable<Country>> GetCountriesAsync();

        Task<Country> GetCountryAsync(int id);

        Task<IEnumerable<OrderDetailTemp>> GetDetailTempsAsync(string userName);

        Task<Order> GetOrdersAsync(int id);

        Task<IEnumerable<Order>> GetOrdersAsync(string userName);

        Product GetProduct(int id);

        IEnumerable<Product> GetProducts();

        Task ModifyOrderDetailTempQuantityAsync(int id, double quantity);

        bool ProductExists(int id);

        Task RemoveCountryAsync(Country country);

        Task RemoveProductAsync(Product product);

        Task<bool> SaveAllAsync();

        Task<int> UpdateCity(City city);

        Task UpdateCountryAsync(Country country);

        Task<Product> UpdateProductAsync(Product product);
    }
}