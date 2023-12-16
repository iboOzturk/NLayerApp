using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Service.Services;
using NLayer.Web.Filters;
using NLayer.Web.Services;

namespace NLayer.Web.Controllers
{

	public class ProductController : Controller
	{
		private readonly ProductApiService _productApiService;
        private readonly CategoryApiService _categoryApiService;

        public ProductController(ProductApiService productApiService, CategoryApiService categoryApiService)
        {
            _productApiService = productApiService;
            _categoryApiService = categoryApiService;
        }

        //private readonly IProductService _productService;
        //private readonly ICategoryService _categoryService;
        //private readonly IMapper _mapper;

        //public ProductController(IProductService productService, ICategoryService categoryService, IMapper mapper)
        //{
        //	_productService = productService;
        //	_categoryService = categoryService;
        //	_mapper = mapper;
        //}

        public async Task<IActionResult> Index()
		{
			//var customResponse = await _productService.GetProductWithCategory();
			var customResponse = await _productApiService.GetProductWithCategoryAsync();

			//return View(customResponse.Data);
			return View(customResponse);
		}
		public async Task<IActionResult> Save()
		{
			//var categories = await _categoryService.GetAllAsync();
			//var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
			//ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name");
			var categoriesDto = await _categoryApiService.GetAllAsync();
			
			ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name");
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Save(ProductDto productDto)
		{
			if (ModelState.IsValid)
			{
				await _productApiService.SaveAsync(productDto);
				//await _productService.AddAsync(_mapper.Map<Product>(productDto));
				return RedirectToAction("Index");
			}
            //var categories = await _categoryService.GetAllAsync();
            //var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            //ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name");
            var categoriesDto = await _categoryApiService.GetAllAsync();

            ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name");
            return View();

		}

		[ServiceFilter(typeof(NotFoundFilter<Product>))]
		public async Task<IActionResult> Update(int id)
		{
			//var product = await _productService.GetByIdAsync(id);
			//var categories = await _categoryService.GetAllAsync();
			//var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
			//ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name", product.CategoryId);
			//return View(_mapper.Map<ProductDto>(product));
			var product = await _productApiService.GetByIdAsync(id);
			var categoriesDto = await _categoryApiService.GetAllAsync();
			
			ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name", product.CategoryId);
			return View(product);
		}
        [HttpPost]
		public async Task<IActionResult> Update(ProductDto productDto)
		{
            //if (ModelState.IsValid)
            //{
            //	await _productService.UpdateAsync(_mapper.Map<Product>(productDto));
            //	return RedirectToAction("Index");
            //}
            //var categories = await _categoryService.GetAllAsync();
            //var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            //ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name", productDto.CategoryId);
            //return View(productDto);
            if (ModelState.IsValid)
            {
               await _productApiService.UpdateAsync(productDto);
                return RedirectToAction("Index");
            }
            var categoriesDto = await _categoryApiService.GetAllAsync();

           
            ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name", productDto.CategoryId);
            return View(productDto);
        }

		public async Task<IActionResult> Delete(int id)
		{
            //var product= await _productService.GetByIdAsync(id);          
            //         await _productService.RemoveAsync(product);
            //         return RedirectToAction("Index");
           await _productApiService.RemoveAsync(id);
            return RedirectToAction("Index");

        }
	}
}