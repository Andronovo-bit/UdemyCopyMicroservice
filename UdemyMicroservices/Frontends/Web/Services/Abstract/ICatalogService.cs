using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models.Catalogs;

namespace Web.Services.Abstract
{
    public interface ICatalogService
    {

        Task<List<CourseViewModel>> GetAllCourseAsync();

        Task<List<CategoryViewModel>> GetAllCategoryAsync();

        Task<List<CategoryViewModel>> GetAllCourseByUserIdAsync(string userId);

        Task<bool> DeleteCourseAsync(string courseId);

        Task<CourseViewModel> GetByCourseId(string coruseId);

        Task<bool> CreateCourseAsync(CourseCreateInput courseCreateInput);

        Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput);

    }
}
