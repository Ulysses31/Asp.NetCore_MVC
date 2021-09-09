using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Asp.NetCoreMVC.Areas.Identity.Pages.Account.Manage
{
	public partial class IndexModel : PageModel
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly IStringLocalizer<IndexModel> _stringLocalizer;

		public IndexModel(
				UserManager<IdentityUser> userManager,
				SignInManager<IdentityUser> signInManager,
				IStringLocalizer<IndexModel> stringLocalizer
		)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_stringLocalizer = stringLocalizer;
		}

		public string Username { get; set; }

		[TempData]
		public string StatusMessage { get; set; }

		[BindProperty]
		public InputModel Input { get; set; }

		public class InputModel
		{
			[Phone]
			[Display(Name = "Phone number")]
			public string PhoneNumber { get; set; }
		}

		private async Task LoadAsync(IdentityUser user)
		{
			var userName = await _userManager.GetUserNameAsync(user);
			var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

			Username = userName;

			Input = new InputModel {
				PhoneNumber = phoneNumber
			};
		}

		public async Task<IActionResult> OnGetAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null) {
				return NotFound($"{_stringLocalizer["OnGetAsync"]} '{_userManager.GetUserId(User)}'.");
			}

			await LoadAsync(user);
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null) {
				return NotFound($"{_stringLocalizer["OnPostAsync"]} '{_userManager.GetUserId(User)}'.");
			}

			if (!ModelState.IsValid) {
				await LoadAsync(user);
				return Page();
			}

			var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
			if (Input.PhoneNumber != phoneNumber) {
				var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
				if (!setPhoneResult.Succeeded) {
					StatusMessage = _stringLocalizer["Error"];
					return RedirectToPage();
				}
			}

			await _signInManager.RefreshSignInAsync(user);
			StatusMessage = _stringLocalizer["Status"];
			return RedirectToPage();
		}
	}
}