using Microsoft.AspNetCore.Mvc;
using Pustovoy.Domain.Entities;
using Pustovoy.UI.Extensions;

namespace Pustovoy.UI.Components
{
	public class CartViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			var cart = HttpContext.Session.Get<Cart>("cart");
			return View(cart);
		}
	}
}
