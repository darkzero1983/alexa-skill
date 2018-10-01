using Alexa.Skill.Context;
using Alexa.Skill.Models.Statistic;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alexa.Skill.Controllers
{
	public class StatisticController : Controller
	{
		[HttpGet]
		public ActionResult Index(int id)
		{
			StatisitcViewModel model = new StatisitcViewModel()
			{
				SkillId = id
			};

			model.Settings.RequestCounts = 20;

			return Index(model);
		}

		[HttpPost]
		public ActionResult Index(StatisitcViewModel model)
		{
			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				Context.Skill skill = db.Skills.FirstOrDefault(x => x.Id == model.SkillId);

				List<RequestLog> logs = db.RequestLogs.Where(x => x.ApplicationId == skill.ApplicationID).OrderByDescending(x => x.RequestTime).Take(model.Settings.RequestCounts).ToList();

				var chartRequests = db.RequestLogs.Where(x => x.ApplicationId == skill.ApplicationID).GroupBy(x => EntityFunctions.TruncateTime(x.RequestTime)).Select(x => new
				{
					Value = x.Count(),
					Day = (DateTime)x.Key
				}).OrderBy(x => x.Day).ToList();

				var allLogs = db.RequestLogs.Where(x => x.ApplicationId == skill.ApplicationID).ToList();

				foreach (var chartRequest in chartRequests)
				{
					model.ChartRequests.Add(new ChartPoint() { Value = chartRequest.Value, Date = chartRequest.Day });
					model.ChartUsers.Add(new ChartPoint() { Value = allLogs.Where(x => x.RequestTime.Date == chartRequest.Day.Date).Select(x => x.UserId).Distinct().Count(), Date = chartRequest.Day });
				}
				foreach (RequestLog log in logs)
				{
					model.RequestLogs.Add(new RequestLogViewModel()
					{
						Information = log.Information,
						Intent = log.Intent,
						IntentParam = log.IntentParam,
						RequestTime = log.RequestTime,
						RequestType = log.RequestType,
						UserId = log.UserId
					});
				}
			}
			return View(model);
		}
	}
}