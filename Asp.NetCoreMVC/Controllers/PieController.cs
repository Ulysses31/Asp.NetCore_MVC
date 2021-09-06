using Asp.NetCoreMVC.Models;
using Asp.NetCoreMVC.Models.ViewModels;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Asp.NetCoreMVC.Controllers
{
	public class PieController : Controller
	{
		private readonly INotyfService _notyf;
		private readonly IPieRepository _pieRepository;
		private readonly ICategoryRepository _categoryRepository;

		public PieController(
			INotyfService notyf,
			IPieRepository pieRepository,
			ICategoryRepository categoryRepository
		)
		{
			this._notyf = notyf;
			this._pieRepository = pieRepository;
			this._categoryRepository = categoryRepository;
		}

		public ViewResult List(string category)
		{
			IEnumerable<Pie> pies;
			string currentCategory;

			if (category != null) {
				pies = _pieRepository.GetAllPies()
					.Where(c => c.Category.CategoryName == category)
					.OrderBy(c => c.PieId);
				currentCategory = _categoryRepository.AllCategories()
					.FirstOrDefault(c => c.CategoryName == category).CategoryName;
			}
			else {
				pies = _pieRepository.GetAllPies().OrderBy(p => p.PieId);
				currentCategory = "All pies";
			}

			return View(new PiesListViewModel() {
				Pies = pies,
				CurrentCategory = currentCategory
			});
		}

		public IActionResult Details(int id)
		{
			var pie = _pieRepository.GetPieById(id);

			if (pie == null) {
				return View("./NotFound", id);
			}

			return View(pie);
		}
	}
}