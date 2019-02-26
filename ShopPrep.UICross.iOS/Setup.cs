namespace ShopPrep.UICross.iOS
{
    using MvvmCross.Platforms.Ios.Core;
    using MvvmCross.ViewModels;
    using ThreeWays.Core;

    public class Setup : MvxIosSetup
    {
        protected override IMvxApplication CreateApp()
        {
            return new App();
        }
    }
}