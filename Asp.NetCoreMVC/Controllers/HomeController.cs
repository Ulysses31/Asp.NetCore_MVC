using Asp.NetCoreMVC.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Security.Claims;

namespace Asp.NetCoreMVC.Controllers
{
	public class HomeController : Controller
	{
		private readonly INotyfService _notyf;
		private readonly ILogger<HomeController> _logger;
		private readonly IPieRepository _pieRepository;
		private readonly IStringLocalizer<HomeController> _stringLocalizer;

		public HomeController(
			INotyfService notyf,
			ILogger<HomeController> logger,
			IPieRepository pieRepository,
			IStringLocalizer<HomeController> stringLocalizer
		)
		{
			this._notyf = notyf;
			this._logger = logger;
			this._pieRepository = pieRepository;
			this._stringLocalizer = stringLocalizer;
		}

		public IActionResult Index()
		{
			var pies = _pieRepository.PiesOfTheWeek();

			ViewData["PageTitle"] = _stringLocalizer["PageTitle"];

			return View(pies);
		}

		public IActionResult SetLanguage(
			string culture, 
			string returnUrl
		)
		{
			Response.Cookies.Append(
				CookieRequestCultureProvider.DefaultCookieName,
				CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
				new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
			);

			return LocalRedirect(returnUrl);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}