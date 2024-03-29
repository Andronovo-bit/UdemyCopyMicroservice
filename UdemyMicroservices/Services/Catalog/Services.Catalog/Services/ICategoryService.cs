﻿using Services.Catalog.Dtos;
using Services.Catalog.Models;
using Shared.Library.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Catalog.Services
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();
        Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto);
        Task<Response<CategoryDto>> GetByIdAsync(string Id);
    }
}
