namespace ShopPrep.UICross.Android.Implementations
{
    using Android.Helpers;
    using Common.Interfaces;
    using global::Android.App;

    public class DroidUserDialogService : AbstractUserDialogService
    {
        public override void Alert(AlertConfig config)
        {
            Utils.RequestMainThread(() =>
                new AlertDialog
                    .Builder(Utils.GetActivityContext())
                    .SetCancelable(false)
                    .SetMessage(config.Message)
                    .SetTitle(config.Title)
                    .SetPositiveButton(config.Button, (o, e) => { })
                    .Show()
            );
        }
    }
}