using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Pustovoy.Domain.Entities;
using Pustovoy.Domain.Models;
using Pustovoy.UI.Controllers;
using Pustovoy.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustovoy.Tests
{
	public class ProductControllerTests
	{
		IProductService _productService;
		ICategoryService _categoryService;
		public ProductControllerTests()
		{
			SetupData();
		}
		// Список категорий сохраняется во ViewData
		[Fact]
		public async void IndexPutsCategoriesToViewData()
		{
			//arrange
			var controller = new ProductController(_categoryService, _productService);
			//act
			var response = await controller.Index(null);
			//assert
			var view = Assert.IsType<ViewResult>(response);
			var categories = Assert.IsType<List<Category>>(view.ViewData["categories"]);
			Assert.Equal(6, categories.Count);
			Assert.Equal("Все", view.ViewData["currentCategory"]);
		}
		// Имя текущей категории сохраняется во ViewData
		[Fact]
		public async void IndexSetsCorrectCurrentCategory()
		{
			//arrange
			var categories = await _categoryService.GetCategoryListAsync();
			var currentCategory = categories.Data[0];
			var controller = new ProductController(_categoryService, _productService);
			//act
			var response = await controller.Index(currentCategory.NormalizedName);
			//assert
			var view = Assert.IsType<ViewResult>(response);
			Assert.Equal(currentCategory.Name, view.ViewData["currentCategory"]);
		}
		// В случае ошибки возвращается NotFoundObjectResult
		[Fact]
		public async void IndexReturnsNotFound()
		{
			//arrange
			string errorMessage = "Test error";
			var categoriesResponse = new ResponseData<List<Category>>();
			categoriesResponse.Success = false;
			categoriesResponse.ErrorMessage = errorMessage;
			_categoryService.GetCategoryListAsync().Returns(Task.FromResult(categoriesResponse));
			var controller = new ProductController(_categoryService, _productService);
			//act
			var response = await controller.Index(null);
			//assert
			var result = Assert.IsType<NotFoundObjectResult>(response);
			Assert.Equal(errorMessage, result.Value.ToString());
		}
		// Настройка имитации ICategoryService и IProductService
		void SetupData()
		{
			_categoryService = Substitute.For<ICategoryService>();
			var categoriesResponse = new ResponseData<List<Category>>();
			categoriesResponse.Data = new List<Category>
			{
				new Category {Id=1, Name="Стартеры", NormalizedName="starters"},
				new Category {Id=2, Name="Салаты", NormalizedName="salads"},
				new Category {Id=3, Name="Супы", NormalizedName="soups"},
				new Category {Id=4, Name="Основные блюда", NormalizedName="main-dishes"},
				new Category {Id=5, Name="Напитки", NormalizedName="drinks"},
				new Category {Id=6, Name="Десерты", NormalizedName="desserts"}
			};
			_categoryService.GetCategoryListAsync().Returns(Task.FromResult(categoriesResponse));
			_productService = Substitute.For<IProductService>();
			var dishes = new List<Dish>
			{
				new Dish { Id = 1 },
				new Dish { Id = 2 },
				new Dish { Id = 3 },
				new Dish { Id = 4 },
				new Dish { Id = 5 }
			};
			var productResponse = new ResponseData<ProductListModel<Dish>>();
			productResponse.Data = new ProductListModel<Dish> { Items = dishes };
			_productService.GetProductListAsync(Arg.Any<string?>(), Arg.Any<int>())
			.Returns(productResponse);
		}

	}
}
