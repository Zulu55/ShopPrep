using System;
using ShopPrep.Common.Models;
using ShopPrep.Common.Services;
using UIKit;

namespace ShopPrep.UIClassic.iOS
{
    public partial class ViewController : UIViewController
    {
        private ApiService apiService;

        protected ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.apiService = new ApiService();

            this.EmailText.Text = "jzuluaga55@gmail.com";
            this.PasswordText.Text = "123456";
            this.ActivityIndicator.StopAnimating();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        partial void LoginButton_TouchUpInside(UIButton sender)
        {
            if (string.IsNullOrEmpty(this.EmailText.Text))
            {
                var alert = UIAlertController.Create("Error", "You must enter an email.", UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("Accept", UIAlertActionStyle.Default, null));
                this.PresentViewController(alert, true, null);
                return;
            }

            if (string.IsNullOrEmpty(this.PasswordText.Text))
            {
                var alert = UIAlertController.Create("Error", "You must enter a password.", UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("Accept", UIAlertActionStyle.Default, null));
                this.PresentViewController(alert, true, null);
                return;
            }

            //var ok = UIAlertController.Create("Ok", "Fuck yeah!", UIAlertControllerStyle.Alert);
            //ok.AddAction(UIAlertAction.Create("Accept", UIAlertActionStyle.Default, null));
            //this.PresentViewController(ok, true, null);

            this.DoLogin();
        }

        private async void DoLogin()
        {
            this.ActivityIndicator.StartAnimating();
            var request = new TokenRequest
            {
                Username = this.EmailText.Text,
                Password = this.PasswordText.Text
            };

            var response = await this.apiService.GetTokenAsync(
                "https://shopprep.azurewebsites.net",
                "/Account",
                "/CreateToken",
                request);

            if (!response.IsSuccess)
            {
                this.ActivityIndicator.StopAnimating();
                var alert = UIAlertController.Create("Error", "User or password incorrect.", UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("Accept", UIAlertActionStyle.Default, null));
                this.PresentViewController(alert, true, null);
                return;
            }

            var token = (TokenResponse)response.Result;
            this.ActivityIndicator.StopAnimating();
            var ok = UIAlertController.Create("Ok", "Fuck yeah!", UIAlertControllerStyle.Alert);
            ok.AddAction(UIAlertAction.Create("Accept", UIAlertActionStyle.Default, null));
            this.PresentViewController(ok, true, null);
        }
    }
}