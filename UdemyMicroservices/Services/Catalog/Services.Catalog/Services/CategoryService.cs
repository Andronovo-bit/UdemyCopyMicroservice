using AutoMapper;
using MongoDB.Driver;
using Services.Catalog.Dtos;
using Services.Catalog.Models;
using Services.Catalog.Settings;
using Shared.Library.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Catalog.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);

            _mapper = mapper;
        }

        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryCollection.Find(category => true).ToListAsync();

            return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);
        }

        public async Task<Response<CategoryDto>> CreateAsync(Category category)
        {
            return await _categoryCollection.InsertOneAsync(category).ContinueWith<Response<CategoryDto>>(cw =>
            {
                if (cw.IsCompletedSuccessfully)
                {
                    return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 204);
                }
                else
                {
                    return Response<CategoryDto>.Fail(cw.Exception.Message, 400);
                }
            });

        }


        public async Task<Response<CategoryDto>> GetByIdAsync(string Id)
        {
            var category = await _categoryCollection.Find<Category>(x => x.Id == Id).FirstOrDefaultAsync();

            if (category == null)
            {
                return Response<CategoryDto>.Fail("Category not found.",404);
            }
            else
            {
                return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
            }
        }
    }
}
