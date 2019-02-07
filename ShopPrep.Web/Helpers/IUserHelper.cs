namespace ShopPrep.Web.Helpers
{
    using System.Threading.Tasks;
    using Data.Entities;

    public interface IUserHelper
    {
        Task<User> GetUserByEmail(string email);
    }
}