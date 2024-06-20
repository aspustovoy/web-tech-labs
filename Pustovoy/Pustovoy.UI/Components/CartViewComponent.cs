using Microsoft.AspNetCore.Mvc;

namespace Pustovoy.UI.Components
{
	public class CartViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
