using Alexa.Skill.Context;
using Alexa.Skill.Models.Home;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alexa.Skill.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Title = "Alex Skill Konfigurator";
			HomeViewModel model = new HomeViewModel();

			string userId = User.Identity.GetUserId();

			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				List<Context.Skill> skills = db.Skills.Where(x => x.UserId == userId).OrderBy(x => x.Name).ToList();
				if(skills != null)
				{
					foreach (Context.Skill skill in skills)
					{
						List<RequestLog> logs = db.RequestLogs.Where(x => x.ApplicationId == skill.ApplicationID).ToList();
						int usageCount = logs.Count();
						int userCount = logs.Select(x => x.UserId).Distinct().Count();
						DateTime yesterday = new DateTime();
						yesterday = DateTime.Now.AddDays(-1);
						int usageCount24h= logs.Where(x => x.RequestTime > yesterday).Count();
						int userCount24h = logs.Where(x => x.RequestTime > yesterday).Select(x => x.UserId).Distinct().Count();

						model.Skills.Add(new SkillOverviewItem()
						{
							Id = skill.Id,
							Name = skill.Name,
							UsageCount = usageCount,
							UserCount = userCount,
							UsageCount24h = usageCount24h,
							UserCount24h = userCount24h
						});
					}
				}
			}
			return View(model);
		}

		public PartialViewResult SkillNameMenu(int skillId)
		{
			SkillNameMenuViewModel model = new SkillNameMenuViewModel()
			{
				SkillId = skillId
			};

			if (skillId == 0)
			{
				return PartialView(model);
			}

			

			string userId = User.Identity.GetUserId();

			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				Context.Skill skill = db.Skills.FirstOrDefault(x => x.Id == skillId && x.UserId == userId);
				if(skill == null)
				{
					return PartialView(model);
				}
				model.SkillName = skill.Name;
			}
			
			return PartialView(model);
		}

		public PartialViewResult MainMenu(int skillId)
		{
			if (skillId == 0)
			{
				return null;
			}

			MainMenuViewModel model = new MainMenuViewModel()
			{
				SkillId = skillId
			};

			string userId = User.Identity.GetUserId();

			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				Context.Skill skill = db.Skills.FirstOrDefault(x => x.Id == skillId && x.UserId == userId);
				if(skill == null)
				{
					return null;
				}
				foreach (Intent intent in skill.Intents.OrderBy(x => x.Name))
				{
					model.Intents.Add(intent.Id, intent.Name);
				}
			}


			return PartialView(model);
		}
	}
}
