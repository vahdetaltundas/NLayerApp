using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using System.Collections.Generic;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductWithDtoController : CustomBaseController
    {
        private readonly IProductServiceWithDto _productServcieWithDto;

        public ProductWithDtoController(IProductServiceWithDto productServcieWithDto)
        {
            _productServcieWithDto = productServcieWithDto;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductWithCategory()
        {

            return CreateActionResult(await _productServcieWithDto.GetProductsWitCategory());
        }




        [HttpGet]
        public async Task<IActionResult> All()
        {
            return CreateActionResult(await _productServcieWithDto.GetAllAsync());
        }

        [ServiceFilter(typeof(NotFountFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResult(await _productServcieWithDto.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductCreateDto dto)
        {
            return CreateActionResult(await _productServcieWithDto.AddAsync(dto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto dto)
        {
            return CreateActionResult(await _productServcieWithDto.UpdateAsync(dto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            return CreateActionResult(await _productServcieWithDto.RemoveAsync(id));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddRange(List<ProductCreateDto> dtos)
        {
            return CreateActionResult(await _productServcieWithDto.AddRangeAsync(dtos));
        }
    }
}
