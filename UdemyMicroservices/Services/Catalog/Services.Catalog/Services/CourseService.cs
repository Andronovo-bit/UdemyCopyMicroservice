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
    public class CourseService : ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CourseService(IMapper mapper , IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            var database = client.GetDatabase(databaseSettings.DatabaseName);
            
            _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);

            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);

            _mapper = mapper;
        }

        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(course => true).ToListAsync();

            if (courses.Any()) //Herhangi bir kayıt varsa
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstOrDefaultAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }

            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Response<CourseDto>> GetByIdAsync(string Id)
        {
            var course = await _courseCollection.Find(x => x.Id == Id).FirstOrDefaultAsync();

            if(course == null)
            {
                return Response<CourseDto>.Fail("Course not found.", 404);
            }

            course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstOrDefaultAsync();

            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
        }

        public async Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId)
        {
            var courses = await _courseCollection.Find(x => x.UserId == userId).ToListAsync();

            if (courses.Any()) 
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstOrDefaultAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }

            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);


        }

        public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
        {
            var newCourse = _mapper.Map<Course>(courseCreateDto);

            newCourse.CreatedTime = DateTime.Now;

            return await _courseCollection.InsertOneAsync(newCourse).ContinueWith<Response<CourseDto>>(cw =>
            {
                if (cw.IsCompletedSuccessfully)
                {
                    return Response<CourseDto>.Success(_mapper.Map<CourseDto>(newCourse), 200);
                }
                else
                {
                    return Response<CourseDto>.Fail(cw.Exception.Message, 500);
                }
            });

        }

        public async Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            var updateCourse = _mapper.Map<Course>(courseUpdateDto);

            return await _courseCollection.FindOneAndReplaceAsync(x => x.Id == courseUpdateDto.Id, updateCourse).ContinueWith<Response<NoContent>>(cw =>
              {
                  if (cw.IsCompletedSuccessfully)
                  {
                      if(cw.Result == null)
                      {
                          return Response<NoContent>.Fail("Course not found.", 404);
                      }

                      return Response<NoContent>.Success(204);
                  }
                  else
                  {
                      return Response<NoContent>.Fail(cw.Exception.Message, 500);
                  }
              });
        }

        public async Task<Response<NoContent>> DeleteAsync(string Id)
        {
            return await _courseCollection.DeleteOneAsync(x=> x.Id == Id).ContinueWith<Response<NoContent>>(cw =>
            {
                if (cw.IsCompletedSuccessfully)
                {
                    if(cw.Result.DeletedCount > 0)
                    {
                        return Response<NoContent>.Success(204);
                    }

                    return Response<NoContent>.Fail("Course not found.",404);

                }
                else
                {
                    return Response<NoContent>.Fail(cw.Exception.Message, 500);
                }
            });
        }
    }

    
}
