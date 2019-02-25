namespace ShopPrep.UICross.iOS.Views
{
    using Common.ViewModels;
    using MvvmCross.Binding.BindingContext;
    using MvvmCross.Platforms.Ios.Presenters.Attributes;
    using MvvmCross.Platforms.Ios.Views;

    [MvxRootPresentation(WrapInNavigationController = true)]
    public partial class HomeView : MvxViewController<LoginViewModel>
    {
        public HomeView() : base("HomeView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<HomeView, LoginViewModel>();
            set.Bind(this.EmailText).To(vm => vm.Email);
            //set.Bind(Button).To(vm => vm.ResetTextCommand);
            set.Apply();
        }
    }
}