using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnityOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
	public class ProductService : Service<Product>, IProductService
	{
		private readonly IProductRepository _productRepository;
		private readonly IMapper _mapper;
		public ProductService(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IMapper mapper, IProductRepository productRepository)
			: base(repository, unitOfWork)
		{
			_mapper = mapper;
			_productRepository = productRepository;
		}

		//MVC için böyle kullanılmalı
		//public async Task<List<ProductWithCategoryDto>> GetProductWithCategory()
		public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductWithCategory()
		{
			var product=await _productRepository.GetProductWithCategory();
			var productDto=_mapper.Map<List<ProductWithCategoryDto>>(product);
			//MVC için böyle kullanılmalı
			//return  productDto;
			return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200,productDto);
		}



	}
}
