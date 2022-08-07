using Microsoft.AspNetCore.Http;
using Shared.Library.Dtos;
using System.Threading.Tasks;
using Web.Models.PhotoStocks;

namespace Web.Services.Abstract
{
    public interface IPhotoStockService
    {
        Task<Response<PhotoViewModel>> UploadPhoto(IFormFile photo);
        Task<bool> DeletePhoto(string photoUrl);
    }
}
