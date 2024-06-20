using Pustovoy.Domain.Entities;
using Pustovoy.Domain.Models;

namespace Pustovoy.UI.Services
{
	public interface ICategoryService
	{
		/// <summary>
		/// Получение списка всех категорий
		/// </summary>
		/// <returns></returns>
		public Task<ResponseData<List<Category>>> GetCategoryListAsync();
	}
}
