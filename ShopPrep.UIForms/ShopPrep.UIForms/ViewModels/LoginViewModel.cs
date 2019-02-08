namespace ShopPrep.UIForms.ViewModels
{
    using Common.Services;
    using GalaSoft.MvvmLight.Command;
    using ShopPrep.Common.Models;
    using System.Windows.Input;
    using Views;
    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel
    {
        private bool isRunning;
        private bool isEnabled;
        private readonly ApiService apiService;

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

        public string Email { get; set; }

        public string Password { get; set; }

        public ICommand LoginCommand => new RelayCommand(this.Login);

        public LoginViewModel()
        {
            this.apiService = new ApiService();
            this.Email = "jzuluaga55@gmail.com";
            this.Password = "123456";
            this.IsEnabled = true;
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter an email.", "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter a password.", "Accept");
                return;
            }

            var request = new TokenRequest
            {
                Password = this.Password,
                Username = this.Email
            };

            this.IsRunning = true;
            this.IsEnabled = false;

            var response = await this.apiService.GetTokenAsync(
                "https://shopprep.azurewebsites.net",
                "/Account",
                "/CreateToken", 
                request);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert("Error", "Email or password incorrect.", "Accept");
                return;
            }

            var token = (TokenResponse)response.Result;
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = token;
            mainViewModel.Products = new ProductsViewModel();

            this.IsRunning = false;
            this.IsEnabled = true;
            Application.Current.MainPage = new MasterPage();
        }
    }
}