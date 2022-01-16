using Shared.Library.Dtos;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Web.Models;
using Web.Models.Catalogs;
using Web.Services.Abstract;

namespace Web.Services.Concrete
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;

        public CatalogService(HttpClient client)
        {
            _client = client;
        }

        public async Task<bool> CreateCourseAsync(CourseCreateInput courseCreateInput)
        {
            var response = await _client.PostAsJsonAsync<CourseCreateInput>("courses", courseCreateInput);

            return response.IsSuccessStatusCode;

        }

        public Task<bool> DeleteCourseAsync(string courseId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
        {
            var response = await _client.GetAsync("categories");
            if (!response.IsSuccessStatusCode) return null;

            var resposeSuccess = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();

            return resposeSuccess.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseAsync()
        {
            var response = await _client.GetAsync("courses");
            if (!response.IsSuccessStatusCode) return null;

            var resposeSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            return resposeSuccess.Data;

        }

        public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
            var response = await _client.GetAsync("categories/GetAllByUserId/" + userId);
            if (!response.IsSuccessStatusCode) return null;

            var resposeSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            return resposeSuccess.Data;
        }

        public async Task<CourseViewModel> GetByCourseId(string courseId)
        {
            var response = await _client.GetAsync("categories/GetById/" + courseId);
            if (!response.IsSuccessStatusCode) return null;

            var resposeSuccess = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();

            return resposeSuccess.Data;
        }

        public async Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput)
        {
            var response = await _client.DeleteAsync("courses/" + courseUpdateInput);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourseAsync(CourseUpdateInput courseUpdateInput)
        {
            var response = await _client.PutAsJsonAsync<CourseUpdateInput>("courses", courseUpdateInput);

            return response.IsSuccessStatusCode;
        }
    }
}
