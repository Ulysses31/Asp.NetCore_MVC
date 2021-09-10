using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCoreMVC.Filters
{
	public class RequireHeaderAttribute : Attribute, IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			if (
				!context.HttpContext.Request.Headers.Keys.Contains("Referer")	||
				!context.HttpContext.Request.Headers["Referer"].Equals("https://localhost:44333/")
			) {
				context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
			}
		}
	}
}
