using Pustovoy.Domain.Entities;
using Pustovoy.Domain.Models;

namespace Pustovoy.UI.Services
{
	public class ApiProductService(HttpClient httpClient) : IProductService
	{
		public async Task<ResponseData<ProductListModel<Dish>>>
		GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
		{
			var uri = httpClient.BaseAddress;
			var queryData = new Dictionary<string, string>();
			queryData.Add("pageNo", pageNo.ToString());
			if (!String.IsNullOrEmpty(categoryNormalizedName))
			{
				queryData.Add("category", categoryNormalizedName);
			}
			var query = QueryString.Create(queryData);
			var result = await httpClient.GetAsync(uri + query.Value);
			if (result.IsSuccessStatusCode)
			{
				return await result.Content
				.ReadFromJsonAsync<ResponseData<ProductListModel<Dish>>>();
			};
			var response = new ResponseData<ProductListModel<Dish>>
			{ Success = false, ErrorMessage = "Ошибка чтения API" };
			return response;
		}

		Task<ResponseData<Dish>> IProductService.CreateProductAsync(Dish product, IFormFile? formFile)
		{
			throw new NotImplementedException();
		}

		Task IProductService.DeleteProductAsync(int id)
		{
			throw new NotImplementedException();
		}

		Task<ResponseData<Dish>> IProductService.GetProductByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		Task IProductService.UpdateProductAsync(int id, Dish product, IFormFile? formFile)
		{
			throw new NotImplementedException();
		}
	}
}
