﻿using Shared.Library.Dtos;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Web.Helpers;
using Web.Models;
using Web.Models.Catalogs;
using Web.Services.Abstract;

namespace Web.Services.Concrete
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;
        private readonly IPhotoStockService _photoStockService;
        private readonly PhotoHelper _photoHelper;

        public CatalogService(HttpClient client, IPhotoStockService photoStockService, PhotoHelper photoHelper)
        {
            _client = client;
            _photoStockService = photoStockService;
            _photoHelper = photoHelper;
        }

        public async Task<bool> CreateCourseAsync(CourseCreateInput courseCreateInput)
        {
            var resultPhoto = await _photoStockService.UploadPhoto(courseCreateInput.PhotoFormFile);

            if (resultPhoto?.IsSuccessful == true)
            {
                courseCreateInput.Picture = resultPhoto.Data.Url;
            }

            var response = await _client.PostAsJsonAsync<CourseCreateInput>("courses", courseCreateInput);

            return response.IsSuccessStatusCode;

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

            resposeSuccess.Data.ForEach(x =>
            {
                x.Picture = _photoHelper.GetPhotoStockUrl(x.Picture);
            });

            return resposeSuccess.Data;

        }

        public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
            var response = await _client.GetAsync($"courses/GetAllByUserId/{userId}");
            if (!response.IsSuccessStatusCode) return null;

            var resposeSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            resposeSuccess.Data.ForEach(x =>
            {
                x.Picture = _photoHelper.GetPhotoStockUrl(x.Picture);
            });


            return resposeSuccess.Data;
        }

        public async Task<CourseViewModel> GetByCourseId(string courseId)
        {
            var response = await _client.GetAsync("courses/" + courseId);
            if (!response.IsSuccessStatusCode) return null;

            var resposeSuccess = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();

            return resposeSuccess.Data;
        }

        public async Task<bool> DeleteCourseAsync(string courseId)
        {
            var response = await _client.DeleteAsync("courses/" + courseId);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput)
        {
            var resultPhoto = await _photoStockService.UploadPhoto(courseUpdateInput.PhotoFormFile);

            if (resultPhoto?.IsSuccessful == true)
            {
                await _photoStockService.DeletePhoto(courseUpdateInput.Picture);
                courseUpdateInput.Picture = resultPhoto.Data.Url;
            }


            var response = await _client.PutAsJsonAsync<CourseUpdateInput>("courses", courseUpdateInput);

            return response.IsSuccessStatusCode;
        }
    }
}
