using Alexa.Skill.Context;
using Alexa.Skill.Models.Intents;
using Alexa.Skill.Models.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alexa.Skill.Controllers
{
	public class IntentController : Controller
	{
		#region Add
		[HttpGet]
		public ActionResult Add(int id)
		{
			IntentAddViewModel model = new IntentAddViewModel()
			{
				SkillId = id
			};


			return View(model);
		}

		[HttpPost]
		public ActionResult Add(IntentAddViewModel model)
		{
			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				Intent intent = new Intent()
				{
					SkillId = model.SkillId,
					Name = model.Name.Replace(" ",""),
					ShouldEndSession = model.ShouldEndSession
				};
				db.Intents.Add(intent);
				db.SaveChanges();
				return RedirectToAction("Detail", "Intent", new { id = intent.Id});
			}
		}
		#endregion

		#region Edit
		[HttpGet]
		public ActionResult Edit(int id)
		{
			IntentEditViewModel model = new IntentEditViewModel();

			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				Intent intent = db.Intents.FirstOrDefault(x => x.Id == id);
				if(intent != null)
				{
					model.IntentId = intent.Id;
					model.Name = intent.Name;
					model.SkillId = intent.SkillId;
					model.ShouldEndSession = intent.ShouldEndSession;
				}
			}
			


			return View(model);
		}

		[HttpPost]
		public ActionResult Edit(IntentEditViewModel model)
		{
			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{

				Intent intent = db.Intents.FirstOrDefault(x => x.Id == model.IntentId);
				if (intent != null)
				{
					intent.Name = model.Name;
					intent.ShouldEndSession = model.ShouldEndSession;
				}
				db.SaveChanges();
				return RedirectToAction("Intents", "Skill", new { id = intent.SkillId });
			}
		}
		#endregion

		#region Detail
		public ActionResult Detail(int id)
		{
			IntentDetailViewModel model = new IntentDetailViewModel();
			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				Intent intent = db.Intents.FirstOrDefault(x => x.Id == id);
				if(intent == null)
				{
					return null;
				}
				
				model.IntentId = id;
				model.Name = intent.Name;
				model.SkillId = intent.Skill.Id;
				model.SkillName = intent.Skill.Name;

				foreach (IntentMessage message in intent.IntentMessages)
				{
					model.Messages.Add(new MessageOverviewItemViewModel()
					{
						MessageId = message.Id,
						Message = message.Message
					});
				}

				foreach (IntentCall intentCall in intent.IntentCalls)
				{
					model.IntentCalls.Add(intentCall.Id, intentCall.CallText);
				}
			}

			return View(model);
		}
		#endregion
	}
}