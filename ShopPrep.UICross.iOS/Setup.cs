namespace ShopPrep.UICross.iOS
{
    using MvvmCross.Platforms.Ios.Core;
    using MvvmCross.ViewModels;

    public class Setup : MvxIosSetup
    {
        protected override IMvxApplication CreateApp()
        {
            return new Common.App();
        }
    }
}