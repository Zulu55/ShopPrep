namespace ShopPrep.UIForms.ViewModels
{
    using System.Linq;
    using System.Windows.Input;
    using Common.Models;
    using Common.Services;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;

    public class EditProductViewModel : BaseViewModel
    {
        private bool isRunning;
        private bool isEnabled;
        private readonly ApiService apiService;

        public Product Product { get; set; }

        public bool IsRunning
        {
            get => this.isRunning;
            set => this.SetValue(ref this.isRunning, value);
        }

        public bool IsEnabled
        {
            get => this.isEnabled;
            set => this.SetValue(ref this.isEnabled, value);
        }

        public ICommand SaveCommand => new RelayCommand(this.Save);

        public ICommand DeleteCommand => new RelayCommand(this.Delete);

        public EditProductViewModel(Product product)
        {
            this.Product = product;
            this.apiService = new ApiService();
            this.IsEnabled = true;
        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(this.Product.Name))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter a product name.", "Accept");
                return;
            }

            if (this.Product.Price <= 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "The price must be a number greather than zero.", "Accept");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            this.Product.UserEmail = MainViewModel.GetInstance().UserEmail;

            var response = await this.apiService.PutAsync(
                "https://shopprep.azurewebsites.net",
                "/api",
                "/Products",
                this.Product.Id,
                this.Product,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            var previousProduct = MainViewModel.GetInstance().Products.Products.Where(p => p.Id == this.Product.Id).FirstOrDefault();
            MainViewModel.GetInstance().Products.Products.Remove(previousProduct);

            var modifiedProduct = (Product)response.Result;
            var newProductItemViewModel = new ProductItemViewModel
            {
                Id = modifiedProduct.Id,
                ImageUrl = modifiedProduct.ImageUrl,
                IsAvailabe = modifiedProduct.IsAvailabe,
                LastPurchase = modifiedProduct.LastPurchase,
                LastSale = modifiedProduct.LastSale,
                Name = modifiedProduct.Name,
                Price = modifiedProduct.Price,
                Stock = modifiedProduct.Stock,
                UserEmail = MainViewModel.GetInstance().UserEmail
            };

            MainViewModel.GetInstance().Products.Products.Add(newProductItemViewModel);

            this.IsRunning = false;
            this.IsEnabled = true;
            await App.Navigator.PopAsync();
        }

        private async void Delete()
        {
            var confirm = await Application.Current.MainPage.DisplayAlert("Confirm", "Are you sure to delete the product?", "Yes", "No");
            if (!confirm)
            {
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var response = await this.apiService.DeleteAsync(
                "https://shopprep.azurewebsites.net",
                "/api",
                "/Products",
                this.Product.Id,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            var previousProduct = MainViewModel.GetInstance().Products.Products.Where(p => p.Id == this.Product.Id).FirstOrDefault();
            MainViewModel.GetInstance().Products.Products.Remove(previousProduct);

            this.IsRunning = false;
            this.IsEnabled = true;
            await App.Navigator.PopAsync();
        }
    }
}