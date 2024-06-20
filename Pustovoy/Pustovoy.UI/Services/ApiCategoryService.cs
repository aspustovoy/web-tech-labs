using Pustovoy.Domain.Entities;
using Pustovoy.Domain.Models;

namespace Pustovoy.UI.Services
{
	public class ApiCategoryService(HttpClient httpClient) : ICategoryService
	{
		public async Task<ResponseData<List<Category>>> GetCategoryListAsync()
		{
			var result = await httpClient.GetAsync(httpClient.BaseAddress);
			if (result.IsSuccessStatusCode)
			{
				return await result.Content
				.ReadFromJsonAsync<ResponseData<List<Category>>>();
			};
			var response = new ResponseData<List<Category>>
			{ Success = false, ErrorMessage = "Ошибка чтения API" };
			return response;
		}
	}
}
