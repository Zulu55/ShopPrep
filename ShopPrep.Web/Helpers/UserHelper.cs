namespace ShopPrep.Web.Helpers
{
    using System.Threading.Tasks;
    using Common.Models;
    using Microsoft.AspNetCore.Identity;

    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> userManager;

        public UserHelper(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await this.userManager.FindByEmailAsync(email);
            return user;
        }
    }
}