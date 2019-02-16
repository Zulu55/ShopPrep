using Android.App;
using Android.OS;
using Android.Widget;
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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.SetContentView(Resource.Layout.LoginPage);
            this.FindViews();
            this.HandleEvents();
        }

        private void HandleEvents()
        {
            this.loginButton.Click += this.LoginButton_Click;
        }

        private void LoginButton_Click(object sender, EventArgs e)
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

            DiaglogService.ShowMessage(this, "Ok", "Fuck yeah!", "Accept");
        }

        private void FindViews()
        {
            this.emailText = this.FindViewById<EditText>(Resource.Id.emailText);
            this.passwordText = this.FindViewById<EditText>(Resource.Id.passwordText);
            this.loginButton = this.FindViewById<Button>(Resource.Id.loginButton);
        }
    }
}