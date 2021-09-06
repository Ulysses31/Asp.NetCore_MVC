using Asp.NetCoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Asp.NetCoreMVC.Components
{
	public class CategoryMenu: ViewComponent
	{
		private readonly ICategoryRepository _categoryRepository;

		public CategoryMenu(ICategoryRepository categoryRepository)
		{
			this._categoryRepository = categoryRepository;
		}

		public IViewComponentResult Invoke()
		{
			var categories = _categoryRepository.AllCategories()
				.OrderBy(c => c.CategoryName);
			return View(categories);
		}
	}
}