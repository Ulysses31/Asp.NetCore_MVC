using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Asp.NetCoreMVC.Areas.Identity.Pages.Account.Manage
{
	public class ChangePasswordModel : PageModel
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly ILogger<ChangePasswordModel> _logger;
		private readonly IStringLocalizer<ChangePasswordModel> _stringLocalizer;

		public ChangePasswordModel(
						UserManager<IdentityUser> userManager,
						SignInManager<IdentityUser> signInManager,
						ILogger<ChangePasswordModel> logger,
						IStringLocalizer<ChangePasswordModel> stringLocalizer
				)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_logger = logger;
			_stringLocalizer = stringLocalizer;
		}

		[BindProperty]
		public InputModel Input { get; set; }

		[TempData]
		public string StatusMessage { get; set; }

		public class InputModel
		{
			[Required(ErrorMessage = "ValidationOldPasswordRequired")]
			[DataType(DataType.Password)]
			[Display(Name = "DisplayCurPassword")]
			public string OldPassword { get; set; }

			[Required(ErrorMessage = "ValidationNewPasswordRequired")]
			[StringLength(100, ErrorMessage = "ValidationNewPasswordString", MinimumLength = 6)]
			[DataType(DataType.Password)]
			[Display(Name = "DisplayNewPassword")]
			public string NewPassword { get; set; }

			[DataType(DataType.Password)]
			[Display(Name = "ValidationConfirmPasswordDisplay")]
			[Compare("NewPassword", ErrorMessage = "ValidationMatchPassword")]
			public string ConfirmPassword { get; set; }
		}

		public async Task<IActionResult> OnGetAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null) {
				return NotFound($"{_stringLocalizer["LoadUserID"]} '{_userManager.GetUserId(User)}'.");
			}

			var hasPassword = await _userManager.HasPasswordAsync(user);
			if (!hasPassword) {
				return RedirectToPage("./SetPassword");
			}

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid) {
				return Page();
			}

			var user = await _userManager.GetUserAsync(User);
			if (user == null) {
				return NotFound($"{_stringLocalizer["LoadUserID"]} '{_userManager.GetUserId(User)}'.");
			}

			var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
			if (!changePasswordResult.Succeeded) {
				foreach (var error in changePasswordResult.Errors) {
					ModelState.AddModelError(string.Empty, error.Description);
				}
				return Page();
			}

			await _signInManager.RefreshSignInAsync(user);
			_logger.LogInformation("User changed their password successfully.");
			StatusMessage = _stringLocalizer["StatusMessage"];

			return RedirectToPage();
		}
	}
}