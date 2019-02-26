namespace ShopPrep.UICross.iOS.Views
{
    using MvvmCross.Binding.BindingContext;
    using MvvmCross.Platforms.Ios.Presenters.Attributes;
    using MvvmCross.Platforms.Ios.Views;
    using ThreeWays.Core.ViewModels;

    [MvxRootPresentation(WrapInNavigationController = true)]
    public partial class HomeView : MvxViewController<TipViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<HomeView, TipViewModel>();
            //set.Bind(this.AmountText).To(vm => vm.SubTotal);
            //set.Bind(this.GenerositySlider).To(vm => vm.Generosity);
            //set.Bind(this.TipLabel).To(vm => vm.Tip);
            set.Apply();
        }
    }
}
