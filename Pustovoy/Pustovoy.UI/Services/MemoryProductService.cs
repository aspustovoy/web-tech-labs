using Pustovoy.Domain.Entities;
using Pustovoy.Domain.Models;

namespace Pustovoy.UI.Services
{
	public class MemoryProductService : IProductService
	{
		List<Dish> _dishes;
		List<Category> _categories;
		public MemoryProductService(ICategoryService categoryService)
		{
			_categories = categoryService.GetCategoryListAsync()
			.Result
			.Data;
			SetupData();
		}

		/// <summary>
		/// Инициализация списков
		/// </summary>
		private void SetupData()
		{
			_dishes = new List<Dish>
			{
				new Dish {Id = 1, Name="Суп-харчо",
				Description="Очень острый, невкусный",
				Calories =200, Image="Images/img1.jpg",
				CategoryId=_categories.Find(c=>c.NormalizedName.Equals("soups")).Id},
				new Dish { Id = 2, Name="Борщ",
				Description="Много сала, без сметаны",
				Calories =330, Image="Images/img2.jpg",
				CategoryId=_categories.Find(c=>c.NormalizedName.Equals("soups")).Id},
				new Dish { Id = 3, Name="Котлета пожарская",
				Description="Хлеб - 80% и Морковь",
				Calories =635, Image="Images/img3.jpg",
				CategoryId=_categories.Find(c=>c.NormalizedName.Equals("mainDishes")).Id},
				new Dish { Id = 4, Name="Макароны по-флотски",
				Description="С охотничьей колбаской",
				Calories =524, Image="Images/img4.jpg",
				CategoryId=_categories.Find(c=>c.NormalizedName.Equals("mainDishes")).Id},
				new Dish { Id = 5, Name="Компот",
				Description="Быстро растворимый, 2 литра",
				Calories =180, Image="Images/img5.jpg",
				CategoryId=_categories.Find(c=>c.NormalizedName.Equals("beverages")).Id},
			};
		}
		public Task<ResponseData<ProductListModel<Dish>>>
		GetProductListAsync(
		string? categoryNormalizedName,
		int pageNo = 1)
		{
            // Создать объект результата
            var result = new ResponseData<ProductListModel<Dish>>();
            // Id категории для фильрации
            int? categoryId = null;
            // если требуется фильтрация, то найти Id категории
            // с заданным categoryNormalizedName
            if (categoryNormalizedName != null)
                categoryId = _categories
                .Find(c => c.NormalizedName.Equals(categoryNormalizedName))?.Id;
            // Выбрать объекты, отфильтрованные по Id категории,
            // если этот Id имеется
            var data = _dishes.Where(d => categoryId == null || d.CategoryId.Equals(categoryId))?
            .ToList();
            // поместить ранные в объект результата
            result.Data = new ProductListModel<Dish>() { Items = data };
            // Если список пустой
            if (data.Count == 0)
            {
                result.Success = false;
                result.ErrorMessage = "Нет объектов в выбраннной категории";
            }
            // Вернуть результат
            return Task.FromResult(result);
        }

		Task<ResponseData<Dish>>? IProductService.GetProductByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		Task IProductService.UpdateProductAsync(int id, Dish product, IFormFile? formFile)
		{
			throw new NotImplementedException();
		}

		Task IProductService.DeleteProductAsync(int id)
		{
			throw new NotImplementedException();
		}

		Task<ResponseData<Dish>> IProductService.CreateProductAsync(Dish product, IFormFile? formFile)
		{
			throw new NotImplementedException();
		}
	}
}
