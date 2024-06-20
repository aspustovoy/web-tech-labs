using Microsoft.AspNetCore.Mvc;
using Pustovoy.Domain.Entities;
using Pustovoy.UI.Services;

namespace Pustovoy.UI.Controllers
{
	public class ProductController(ICategoryService categoryService, IProductService productService) : Controller
	{
		public async Task<IActionResult> Index(string? category)
        {
            // получить список категорий
            var categoriesResponse = await categoryService.GetCategoryListAsync();
            // если список не получен, вернуть код 404
            if (!categoriesResponse.Success)
                return NotFound(categoriesResponse.ErrorMessage);
            // передать список категорий во ViewData
            ViewData["categories"] = categoriesResponse.Data;
            // передать во ViewData имя текущей категории
            var currentCategory = category == null ? "Все" : categoriesResponse
                .Data.FirstOrDefault(c => c.NormalizedName == category)?.Name;
            ViewData["currentCategory"] = currentCategory;
            var productResponse = await productService.GetProductListAsync(category);
            if (!productResponse.Success)
                ViewData["Error"] = productResponse.ErrorMessage;
            return View(productResponse.Data.Items);
        }
	}
}
