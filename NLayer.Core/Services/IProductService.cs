using NLayer.Core.DTOs;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
	public interface IProductService:IService<Product>
	{
        //MVC için böyle kullanılmalı
        //Task<List<ProductWithCategoryDto>> GetProductWithCategory();	
        Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductWithCategory();	
     

	}
}
