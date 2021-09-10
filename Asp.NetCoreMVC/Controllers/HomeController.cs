using Asp.NetCoreMVC.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Asp.NetCoreMVC.Controllers
{
	// [RequireHeader] // Filter RequireHeaderAttribute.cs
	// [TimerAction()]
	[ResponseCache(Duration = 20)]
	public class HomeController : Controller
	{
		private readonly INotyfService _notyf;
		private readonly ILogger<HomeController> _logger;
		private readonly IPieRepository _pieRepository;
		private readonly IStringLocalizer<HomeController> _stringLocalizer;
		private readonly IMemoryCache _memoryCache;
		private readonly TelemetryClient _tc;

		public HomeController(
			INotyfService notyf,
			ILogger<HomeController> logger,
			IPieRepository pieRepository,
			IStringLocalizer<HomeController> stringLocalizer,
			IMemoryCache memoryCache,
			TelemetryClient tc
		)
		{
			this._notyf = notyf;
			this._logger = logger;
			this._pieRepository = pieRepository;
			this._stringLocalizer = stringLocalizer;
			this._memoryCache = memoryCache;
			this._tc = tc;
		}

		public IActionResult Index()
		{
			// Log
			_logger.LogError("Home loaded...");

			// Application Insights
			_tc.TrackPageView(
				new PageViewTelemetry("Asp.NetCoreMVC Page View Insights") { 
					Timestamp = DateTime.UtcNow 
				}
			);
			_tc.TrackEvent("HomeControllerLoad");

			var pies = _pieRepository.PiesOfTheWeek();

			ViewData["PageTitle"] = _stringLocalizer["PageTitle"];

			// Caching
			List<Pie> piesOfTheWeekCached = null;

			if (!_memoryCache.TryGetValue(
				CacheEntryConstants.PiesOfTheWeek, 
				out piesOfTheWeekCached
			)) {
				piesOfTheWeekCached = _pieRepository.PiesOfTheWeek().ToList();
				var cacheEntryOptions = new MemoryCacheEntryOptions()
					.SetSlidingExpiration(TimeSpan.FromSeconds(5));
				cacheEntryOptions.RegisterPostEvictionCallback(FillCacheAgain, this);

				_memoryCache.Set(
					CacheEntryConstants.PiesOfTheWeek, 
					piesOfTheWeekCached, 
					cacheEntryOptions
				);
			}

			// Caching
			// List<Pie> piesCached = _memoryCache.GetOrCreate(CacheEntryConstants.PiesOfTheWeek, entry => {
			// 	entry.SlidingExpiration = TimeSpan.FromSeconds(10);
			// 	entry.Priority = CacheItemPriority.High;
			// 	return _pieRepository.PiesOfTheWeek().ToList();
			// });

			return View(piesOfTheWeekCached);
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

		private void FillCacheAgain(
			object key, 
			object value, 
			EvictionReason reason, 
			object state
		)
		{
			Debug.WriteLine("HomeController", "Cache was cleared: reason " + reason.ToString());
		}
	}
}