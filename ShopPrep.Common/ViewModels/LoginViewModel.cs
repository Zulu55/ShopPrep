namespace ShopPrep.Common.ViewModels
{
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using ShopPrep.Common.Services;
    using System.Windows.Input;

    public class LoginViewModel : MvxViewModel
    {
        private string email;
        private string password;
        private MvxCommand loginCommand;
        private readonly IApiService apiService;
        private readonly IMvxNavigationService navigationService;

        public string Email
        {
            get => this.email;
            set
            {
                this.email = value;
                this.RaisePropertyChanged(() => this.Email);
            }
        }

        public string Password
        {
            get => this.password;
            set
            {
                this.password = value;
                this.RaisePropertyChanged(() => this.Password);
            }
        }

        public ICommand LoginCommand
        {
            get
            {
                this.loginCommand = this.loginCommand ?? new MvxCommand(this.DoLoginCommand);
                return this.loginCommand;
            }
        }

        public LoginViewModel(IApiService apiService, IMvxNavigationService navigationService)
        {
            this.apiService = apiService;
            this.navigationService = navigationService;
        }

        private void DoLoginCommand()
        {
            if (string.IsNullOrEmpty(this.Email))
            {

            }
        }
    }
}