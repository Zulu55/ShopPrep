namespace ShopPrep.Web.Helpers
{
    using System.Threading.Tasks;
    using Common.Models;

    public interface IUserHelper
    {
        Task<User> GetUserByEmail(string email);
    }
}