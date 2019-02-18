using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using ShopPrep.Common.Models;
using ShopPrep.Common.Services;
using ShopPrep.UIClassic.Android.Helpers;
using System;

namespace ShopPrep.UIClassic.Android.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class LoginActivity : Activity
    {
        private EditText emailText;
        private EditText passwordText;
        private Button loginButton;
        private ApiService apiService;
        private ProgressBar activityIndicatorProgressBar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.SetContentView(Resource.Layout.LoginPage);
            this.FindViews();
            this.HandleEvents();
            this.SetInitialData();
        }

        private void SetInitialData()
        {
            this.apiService = new ApiService();
            this.emailText.Text = "jzuluaga55@gmail.com";
            this.passwordText.Text = "123456";
            this.activityIndicatorProgressBar.Visibility = ViewStates.Gone;
        }

        private void HandleEvents()
        {
            this.loginButton.Click += this.LoginButton_Click;
        }

        private async void LoginButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.emailText.Text))
            {
                DiaglogService.ShowMessage(this, "Error", "You must enter an email.", "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.passwordText.Text))
            {
                DiaglogService.ShowMessage(this, "Error", "You must enter a password.", "Accept");
                return;
            }

            this.activityIndicatorProgressBar.Visibility = ViewStates.Visible;
            var request = new TokenRequest
            {
                Password = this.passwordText.Text,
                Username = this.emailText.Text
            };

            var response = await this.apiService.GetTokenAsync(
                "https://shopprep.azurewebsites.net",
                "/Account",
                "/CreateToken",
                request);

            if (!response.IsSuccess)
            {
                DiaglogService.ShowMessage(this, "Error", "User or password incorrect.", "Accept");
                return;
            }

            this.activityIndicatorProgressBar.Visibility = ViewStates.Gone;

            //DiaglogService.ShowMessage(this, "Ok", "Fuck Yeah!", "Accept");

            var token = (TokenResponse)response.Result;
            var intent = new Intent(this, typeof(ProductsActivity));
            intent.PutExtra("token", JsonConvert.SerializeObject(token));
            intent.PutExtra("email", this.emailText.Text);
            this.StartActivity(intent);
        }

        private void FindViews()
        {
            this.emailText = this.FindViewById<EditText>(Resource.Id.emailText);
            this.passwordText = this.FindViewById<EditText>(Resource.Id.passwordText);
            this.loginButton = this.FindViewById<Button>(Resource.Id.loginButton);
            this.activityIndicatorProgressBar = this.FindViewById<ProgressBar>(Resource.Id.activityIndicatorProgressBar);
        }
    }
}