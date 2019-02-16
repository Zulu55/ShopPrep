namespace ShopPrep.UIForms.ViewModels
{
    using System.Windows.Input;
    using Common.Services;
    using GalaSoft.MvvmLight.Command;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using ShopPrep.Common.Models;
    using ShopPrep.UIForms.Helpers;
    using Xamarin.Forms;

    public class AddProductViewModel : BaseViewModel
    {
        private bool isRunning;
        private bool isEnabled;
        private ImageSource imageSource;
        private readonly ApiService apiService;
        private MediaFile file;

        public ImageSource ImageSource
        {
            get => this.imageSource;
            set => this.SetValue(ref this.imageSource, value);
        }

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

        public string Name { get; set; }

        public string Price { get; set; }

        public ICommand SaveCommand => new RelayCommand(this.Save);

        public ICommand ChangeImageCommand => new RelayCommand(this.ChangeImage);

        public AddProductViewModel()
        {
            this.apiService = new ApiService();
            this.ImageSource = "noImage";
            this.IsEnabled = true;
        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter a product name.", "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Price))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter a product price.", "Accept");
                return;
            }

            var price = decimal.Parse(this.Price);
            if (price <= 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "The price must be a number greather than zero.", "Accept");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            byte[] imageArray = null;
            if (this.file != null)
            {
                imageArray = FilesHelper.ReadFully(this.file.GetStream());
            }

            var product = new Product
            {
                IsAvailabe = true,
                Name = this.Name,
                Price = price,
                UserEmail = MainViewModel.GetInstance().UserEmail,
                ImageArray = imageArray
            };

            var response = await this.apiService.PostAsync(
                "https://shopprep.azurewebsites.net",
                "/api",
                "/Products",
                product,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            var newProduct = (Product)response.Result;
            var newProductItemViewModel = new ProductItemViewModel
            {
                Id = newProduct.Id,
                ImageUrl = newProduct.ImageUrl,
                IsAvailabe = newProduct.IsAvailabe,
                LastPurchase = newProduct.LastPurchase,
                LastSale = newProduct.LastSale,
                Name = newProduct.Name,
                Price = newProduct.Price,
                Stock = newProduct.Stock,
                UserEmail = newProduct.UserEmail
            };

            MainViewModel.GetInstance().Products.Products.Add(newProductItemViewModel);

            this.IsRunning = false;
            this.IsEnabled = true;
            await App.Navigator.PopAsync();
        }

        private async void ChangeImage()
        {
            await CrossMedia.Current.Initialize();

            var source = await Application.Current.MainPage.DisplayActionSheet(
                "Where do you take the picture?",
                "Cancel",
                null,
                "From Gallery",
                "From Camera");

            if (source == "Cancel")
            {
                this.file = null;
                return;
            }

            if (source == "From Camera")
            {
                this.file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    }
                );
            }
            else
            {
                this.file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (this.file != null)
            {
                this.ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }
        }
    }
}