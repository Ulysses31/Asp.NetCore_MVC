using Asp.NetCoreMVC.Models;
using Asp.NetCoreMVC.Models.ViewModels;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Asp.NetCoreMVC.Controllers
{
	public class PieController : Controller
	{
		private readonly INotyfService _notyf;
		private readonly IPieRepository _pieRepository;
		private readonly IPieReviewRepository _pieReviewRepository;
		private readonly ICategoryRepository _categoryRepository;
		private readonly HtmlEncoder _htmlEncoder;

		public PieController(
			INotyfService notyf,
			IPieRepository pieRepository,
			IPieReviewRepository pieReviewRepository,
			ICategoryRepository categoryRepository,
			HtmlEncoder htmlEncoder
		)
		{
			this._notyf = notyf;
			this._pieRepository = pieRepository;
			this._pieReviewRepository = pieReviewRepository;
			this._categoryRepository = categoryRepository;
			this._htmlEncoder = htmlEncoder;
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

		[Route("[controller]/Details/{id}")]
		public IActionResult Details(int id)
		{
			var pie = _pieRepository.GetPieById(id);

			if (pie == null) {
				return View("./NotFound", id);
			}

			return View(new PieDetailViewModel() {
				Pie = pie
			});
		}

		[Route("[controller]/Details/{id}")]
		[HttpPost]
		public IActionResult Details(int id, string review)
		{
			if(string.IsNullOrEmpty(review)) {
				ModelState.AddModelError("", "Review is required.");
			}

			var pie = _pieRepository.GetPieById(id);

			if (pie == null) {
				return NotFound();
			}

			if (ModelState.IsValid) {
				_pieReviewRepository.AddPieReview(new PieReview() {
					Pie = pie,
					Review = _htmlEncoder.Encode(review),
					UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value
				});
				_notyf.Success("Review saved!");
			}

			return View(new PieDetailViewModel() {
				Pie = _pieRepository.GetPieById(id)
		});
		}
	}
}