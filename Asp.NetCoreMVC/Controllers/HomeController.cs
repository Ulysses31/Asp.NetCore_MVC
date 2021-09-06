using Asp.NetCoreMVC.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
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

		public HomeController(
			INotyfService notyf,
			ILogger<HomeController> logger,
			IPieRepository pieRepository
		)
		{
			this._notyf = notyf;
			this._logger = logger;
			this._pieRepository = pieRepository;
		}

		public IActionResult Index()
		{
			var pies = _pieRepository.PiesOfTheWeek();
			return View(pies);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}