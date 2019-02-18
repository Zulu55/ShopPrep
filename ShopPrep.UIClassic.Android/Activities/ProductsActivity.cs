using Android.App;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using ShopPrep.Common.Models;
using ShopPrep.Common.Services;
using ShopPrep.UIClassic.Android.Adapters;
using ShopPrep.UIClassic.Android.Helpers;
using System.Collections.Generic;

namespace ShopPrep.UIClassic.Android.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class ProductsActivity : Activity
    {
        private TokenResponse token;
        private string email;
        private ApiService apiService;
        private ListView productsListView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.SetContentView(Resource.Layout.ProductsPage);

            this.productsListView = FindViewById<ListView>(Resource.Id.productsListView);

            this.email = Intent.Extras.GetString("email");
            var tokenString = Intent.Extras.GetString("token");
            this.token = JsonConvert.DeserializeObject<TokenResponse>(tokenString);

            this.apiService = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            var response = await this.apiService.GetListAsync<Product>(
                "https://shopprep.azurewebsites.net",
                "/api",
                "/Products",
                "bearer",
                this.token.Token);

            if (!response.IsSuccess)
            {
                DiaglogService.ShowMessage(this, "Error", response.Message, "Accept");
                return;
            }

            var products = (List<Product>)response.Result;
            this.productsListView.Adapter = new ProductsListAdapter(this, products);
            this.productsListView.FastScrollEnabled = true;
        }
    }
}