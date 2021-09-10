using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Asp.NetCoreMVC.Filters
{
	public class TimerActionAttribute : ActionFilterAttribute
	{
		private readonly Stopwatch _stopWatch = new Stopwatch();

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			_stopWatch.Reset();
			_stopWatch.Start();

			// var elapsedBytes = Encoding.ASCII.GetBytes(_stopWatch.Elapsed.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
			var elapsed = _stopWatch.Elapsed.TotalMilliseconds.ToString(CultureInfo.InvariantCulture);
			//_logger.LogInformation("Action executed - elapsed time: " + elapsed);
		}

		public override void OnActionExecuted(ActionExecutedContext context)
		{
			_stopWatch.Stop();
			// var elapsed = Encoding.ASCII.GetBytes(_stopWatch.Elapsed.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
			var elapsed = _stopWatch.Elapsed.TotalMilliseconds.ToString(CultureInfo.InvariantCulture);
			//_logger.LogInformation("Action executed - elapsed time: " + elapsed);
		}

		public override void OnResultExecuted(ResultExecutedContext context)
		{
			base.OnResultExecuted(context);
			_stopWatch.Stop();
			var elapsed = Encoding.ASCII.GetBytes(_stopWatch.Elapsed.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
			//context.HttpContext.Response.Body.Write(elapsed, 0, elapsed.Length);
		}
	}
}