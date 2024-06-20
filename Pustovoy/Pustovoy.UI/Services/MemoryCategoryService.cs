using Pustovoy.Domain.Entities;
using Pustovoy.Domain.Models;

namespace Pustovoy.UI.Services
{
	public class MemoryCategoryService : ICategoryService
	{
		public Task<ResponseData<List<Category>>>GetCategoryListAsync()
		{
			var categories = new List<Category>
			{
				new Category {Id=1, Name="Салаты",
				NormalizedName="salads"},
				new Category {Id=2, Name="Супы",
				NormalizedName="soups"},
				new Category {Id=3, Name="Основные блюда",
				NormalizedName="mainDishes"},
				new Category {Id=4, Name="Напитки",
				NormalizedName="beverages"},
				new Category {Id=5, Name="Десерты",
				NormalizedName="dessert"},
			};
			var result = new ResponseData<List<Category>>();
			result.Data = categories;
			return Task.FromResult(result);
		}
	}
}
