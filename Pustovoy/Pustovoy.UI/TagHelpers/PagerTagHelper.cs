using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;

namespace Pustovoy.UI.TagHelpers
{
	public class PagerTagHelper(LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor) : TagHelper
	{
		// номер текущей страницы
		public int CurrentPage { get; set; }
		// общее количество страниц
		public int TotalPages { get; set; }
		// имя категории объектов
		public string? Category { get; set; }
		// признак страниц администратора
		public bool Admin { get; set; } = false;
		// Номер предыдущей страницы
		int Prev
		{
			get => CurrentPage == 1
			? 1
			: CurrentPage - 1;
		}
		// Номер следующей страницы
		int Next
		{
			get => CurrentPage == TotalPages
			? TotalPages
			: CurrentPage + 1;
		}
		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = "div";
			output.AddClass("row", HtmlEncoder.Default);
			var nav = new TagBuilder("nav");
			nav.Attributes.Add("aria-label", "pagination");
			var ul = new TagBuilder("ul");
			ul.AddCssClass("pagination");
			#region Кнопка предыдущей страницы
			ul.InnerHtml.AppendHtml(
			CreateListItem(Category, Prev, "<span aria-hidden =\"true\">&laquo;</span>"));
			#endregion Кнопка предыдущей страницы
			#region Разметка кнопок переключения страниц
			for (var index = 1; index <= TotalPages; index++)
			{
				ul.InnerHtml.AppendHtml(
				CreateListItem(Category, index, String.Empty));
			}
			#endregion Разметка кнопок переключения страниц
			#region Кнопка следующей страницы
			ul.InnerHtml.AppendHtml(
			CreateListItem(Category, Next, "<span aria-hidden =\"true\">&raquo;</span>"));
			#endregion Кнопка следующей страницы
			nav.InnerHtml.AppendHtml(ul);
			output.Content.AppendHtml(ul);
		}
		/// <summary>
		/// Разметка одной кнопки пейджера
		/// </summary>
		/// <param name="category1">имя категории</param>
		/// <param name="pageNo">номер страницы</param>
		/// <param name="innerText">текст кнопки</param>
		/// <returns></returns>
		TagBuilder CreateListItem(string? category1, int pageNo, string? innerText)
		{
			var li = new TagBuilder("li");
			li.AddCssClass("page-item");
			if (pageNo == CurrentPage &&
			String.IsNullOrEmpty(innerText))
				li.AddCssClass("active");
			var a = new TagBuilder("a");
			a.AddCssClass("page-link");
			var routeData = new
			{
				pageno = pageNo,
				category = category1
			};
			string url;
			// Для страниц администратора будет использоваться не MVC, а Razor pages
			if (Admin)
				url = linkGenerator.GetPathByPage(httpContextAccessor.HttpContext, page:"./Index", values: routeData);
			else
				url = linkGenerator.GetPathByAction("index", "product", routeData);
			a.Attributes.Add("href", url);
			var text = String.IsNullOrEmpty(innerText)
				? pageNo.ToString()
				: innerText;
			a.InnerHtml.AppendHtml(text);
			li.InnerHtml.AppendHtml(a);
			return li;
		}
	}
}
