using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnityOfWorks;
using NLayer.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
	public class CategoryService : Service<Category>, ICategoryService
	{
		private readonly ICategoryRepository _categoryRepository;
		private readonly IMapper _mapper;
		public CategoryService(IGenericRepository<Category> repository, IUnitOfWork unitOfWork, IMapper mapper,
			ICategoryRepository categoryRepository) : base(repository, unitOfWork)
		{
			_mapper = mapper;
			_categoryRepository = categoryRepository;
		}

		public async Task<CustomResponseDto<CategoryWithProductsDto>> GetSingleCategoryByIdWithProductsAsync(int categoryId)
		{
			var hasCategory = await _categoryRepository.GetSingleCategoryByIdWithProductsAsync(categoryId);
			if (hasCategory == null)
			{
				throw new NotFoundException($"{typeof(Category).Name}({categoryId}) not found");
			}
			var categoryDto=_mapper.Map<CategoryWithProductsDto>(hasCategory);
			return CustomResponseDto<CategoryWithProductsDto>.Success(200, categoryDto);
		}
	}
}
