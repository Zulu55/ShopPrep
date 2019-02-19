namespace ShopPrep.UICross.Android.Views
{
    using global::Android.App;
    using global::Android.OS;
    using MvvmCross.Platforms.Android.Views;
    using ShopPrep.Common.ViewModels;

    [Activity(Label = "@string/app_name")]
    public class LoginView : MvxActivity<LoginViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.SetContentView(Resource.Layout.LoginPage);
        }
    }
}