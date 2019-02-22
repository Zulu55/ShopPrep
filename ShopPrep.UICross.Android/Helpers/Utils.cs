namespace ShopPrep.UICross.Android.Helpers
{
    using System;
    using System.Threading;
    using global::Android.App;
    using global::Android.Content;
    using MvvmCross;
    using MvvmCross.Platforms.Android;

    public static class Utils
    {

        public static void RequestMainThread(Action action)
        {
            if (Application.SynchronizationContext == SynchronizationContext.Current)
            {
                action();
            }
            else
            {
                Application.SynchronizationContext.Post(x => MaskException(action), null);
            }
        }


        public static void MaskException(Action action)
        {
            try
            {
                action();
            }
            catch { }
        }


        public static Context GetActivityContext()
        {
            return Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
        }
    }
}