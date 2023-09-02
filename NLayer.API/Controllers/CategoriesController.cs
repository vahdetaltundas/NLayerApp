using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var categorys= await _categoryService.GetAllAsync();
            var categoryDto = _mapper.Map<List<CategoryDto>>(categorys.ToList());
            return CreateActionResult(CustomResponseDto<List<CategoryDto>>.Success(200,categoryDto));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetSingleGategoryByIdWithProduct(int categoryId)
        {
            return CreateActionResult(await _categoryService.GetSingleGategoryByIdWithProductAsync(categoryId));
        }
    }
}
