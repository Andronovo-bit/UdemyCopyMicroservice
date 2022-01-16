using System.Threading.Tasks;
using Web.Models;

namespace Web.Services.Abstract
{
    public interface IUserService
    {
        Task<UserViewModel> GetUser();
    }
}
