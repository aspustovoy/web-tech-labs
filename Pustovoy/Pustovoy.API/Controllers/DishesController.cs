using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustovoy.API.Data;
using Pustovoy.Domain.Entities;
using Pustovoy.Domain.Models;

namespace Pustovoy.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DishesController : ControllerBase
	{
		private readonly AppDbContext _context;

		public DishesController(AppDbContext context)
		{
			_context = context;
		}

		// GET: api/Dishes
		[HttpGet]
		public async Task<ActionResult<ResponseData<ProductListModel<Dish>>>> GetDishes(
		string? category,
		int pageNo = 1,
		int pageSize = 3)
		{
			// Создать объект результата
			var result = new ResponseData<ProductListModel<Dish>>();
			// Фильтрация по категории загрузка данных категории
			var data = _context.Dishes
				.Include(d => d.Category)
				.Where(d => String.IsNullOrEmpty(category)
					|| d.Category.NormalizedName.Equals(category));
			// Подсчет общего количества страниц
			int totalPages = (int)Math.Ceiling(data.Count() / (double)pageSize);
			if (pageNo > totalPages)
				pageNo = totalPages;
			// Создание объекта ProductListModel с нужной страницей данных
			var listData = new ProductListModel<Dish>()
			{
				Items = await data
					.OrderBy(d => d.Id)
					.Skip((pageNo - 1) * pageSize)
					.Take(pageSize)
					.ToListAsync(),
				CurrentPage = pageNo,
				TotalPages = totalPages,
			};

			// поместить данные в объект результата
			result.Data = listData;
			// Если список пустой
			if (data.Count() == 0)
			{
				result.Success = false;
				result.ErrorMessage = "Нет объектов в выбранной категории";
			}
			return result;
		}






		// GET: api/Dishes/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Dish>> GetDish(int id)
		{
			var dish = await _context.Dishes.FindAsync(id);

			if (dish == null)
			{
				return NotFound();
			}

			return dish;
		}

		// PUT: api/Dishes/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutDish(int id, Dish dish)
		{
			if (id != dish.Id)
			{
				return BadRequest();
			}

			_context.Entry(dish).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!DishExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Dishes
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Dish>> PostDish(Dish dish)
		{
			_context.Dishes.Add(dish);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetDish", new { id = dish.Id }, dish);
		}

		// DELETE: api/Dishes/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteDish(int id)
		{
			var dish = await _context.Dishes.FindAsync(id);
			if (dish == null)
			{
				return NotFound();
			}

			_context.Dishes.Remove(dish);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool DishExists(int id)
		{
			return _context.Dishes.Any(e => e.Id == id);
		}
	}
}
