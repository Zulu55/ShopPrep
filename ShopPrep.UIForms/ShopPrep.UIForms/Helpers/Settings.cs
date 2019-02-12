namespace ShopPrep.UIForms.Helpers
{
    using Plugin.Settings;
    using Plugin.Settings.Abstractions;

    public static class Settings
    {
        private const string token = "token";
        private const string userEmail = "userEmail";
        private const string isRemember = "isRemember";
        private static readonly string stringDefault = string.Empty;
        private static readonly bool boolDefault = false;

        private static ISettings AppSettings => CrossSettings.Current;

        public static string Token
        {
            get => AppSettings.GetValueOrDefault(token, stringDefault);
            set => AppSettings.AddOrUpdateValue(token, value);
        }

        public static string UserEmail
        {
            get => AppSettings.GetValueOrDefault(userEmail, stringDefault);
            set => AppSettings.AddOrUpdateValue(userEmail, value);
        }

        public static bool IsRemember
        {
            get => AppSettings.GetValueOrDefault(isRemember, boolDefault);
            set => AppSettings.AddOrUpdateValue(isRemember, value);
        }
    }
}