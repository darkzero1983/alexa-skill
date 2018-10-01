using Alexa.Skill.Context;
using Alexa.Skill.Models.Intents;
using Alexa.Skill.Models.Skills;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Alexa.Skill.Controllers
{
	public class SkillController : Controller
	{
		#region Add
		[HttpGet]
		public ActionResult Add()
		{
			
			SkillAddViewModel model = new SkillAddViewModel()
			{
				SkillTypes = SkillList()
			};
			
			return View(model);
		}

		[HttpPost]
		public ActionResult Add(SkillAddViewModel model)
		{
			string userId = User.Identity.GetUserId();
			if (ModelState.IsValid)
			{
				
				using (AlexaSkillEntities db = new AlexaSkillEntities())
				{
					db.Skills.Add(new Context.Skill()
					{
						ApplicationID = model.ApplicationID,
						Name = model.Name,
						UserId = userId,
						SkillType = model.SkillTypeId
					});
					db.SaveChanges();
					return RedirectToAction("Index", "Home");
				}
			}
			model.SkillTypes = SkillList();
			return View(model);
		}
		#endregion

		#region Detail
		[HttpGet]
		public ActionResult Detail(int id)
		{
			string userId = User.Identity.GetUserId();
			SkillDetailModel model = new SkillDetailModel()
			{
				Id = id
			};

			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				Context.Skill skill = db.Skills.FirstOrDefault(x => x.Id == id && x.UserId == userId);
				if(skill == null)
				{
					return null;
				}
				model.Name = skill.Name;
				model.SkillType = (Enums.SkillType)Enum.ToObject(typeof(Enums.SkillType), skill.SkillType) ;
			}

			return View(model);
		}
		#endregion

		#region Intents
		[HttpGet]
		public ActionResult Intents(int id)
		{
			string userId = User.Identity.GetUserId();
			IntentOverviewViewModel model = new IntentOverviewViewModel()
			{
				SkillId = id
			};
			string skillName;
			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				Context.Skill skill = db.Skills.FirstOrDefault(x => x.Id == id && x.UserId == userId);
				if (skill == null)
				{
					return null;
				}
				skillName = skill.Name;
				List<Intent> intents = db.Intents.Where(x => x.SkillId == skill.Id).ToList();

				foreach (Intent intent in intents)
				{
					model.Intents.Add(new IntentItemViewModel()
					{
						Id = intent.Id,
						Name = intent.Name
					});
				}
			}
			

			return View(model);
		}
		#endregion

		#region WelcomeMessages
		public ActionResult WelcomeMessages(int id)
		{
			SkillWelcomeMessagesOverviewViewModel model = new SkillWelcomeMessagesOverviewViewModel();
			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				Context.Skill skill = db.Skills.FirstOrDefault(x => x.Id == id);
				if (skill == null)
				{
					return null;
				}
				model.SkillId = skill.Id;

				foreach (WelcomeMessage welcomeMessage in skill.WelcomeMessages)
				{
					model.Messages.Add(new Models.Messages.MessageOverviewItemViewModel
					{
						MessageId = welcomeMessage.Id,
						Message = welcomeMessage.Message
					});
				}
			}

			return View(model);
		}
		#endregion

		#region Private Functions
		private List<SelectListItem> SkillList()
		{
			List<SelectListItem> skills = new List<SelectListItem>();
			string userId = User.Identity.GetUserId();

			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				foreach (SkillType skillType in db.SkillTypes.Where(x => x.UserId == null || x.UserId == userId))
				{
					skills.Add(new SelectListItem()
					{
						Text = skillType.Name,
						Value = skillType.Id.ToString()
					});
				}
			}

			return skills;
		}
		#endregion
	}
}