using Alexa.Skill.Context;
using Alexa.Skill.Models.Messages;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Alexa.Skill.Controllers
{
	public class IntentCallController : Controller
	{
		#region Add
		[HttpGet]
		public ActionResult Add(int id)
		{
			IntentCallEditViewModel model = new IntentCallEditViewModel()
			{
				IntentId = id
			};
			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				Intent intent = db.Intents.FirstOrDefault(x => x.Id == id);
				if (intent != null)
				{
					model.SkillId = intent.SkillId;
				}
			}

			return View("Edit", model);
		}
		#endregion

		#region Edit
		[HttpGet]
		public ActionResult Edit(int id)
		{
			IntentCallEditViewModel model = new IntentCallEditViewModel();

			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				IntentCall intentCall = db.IntentCalls.FirstOrDefault(x => x.Id == id);
				if (intentCall != null)
				{
					model.IntentCallId = intentCall.Id;
					model.CallText = intentCall.CallText;
					model.IntentId = intentCall.IntentId;
					model.SkillId = intentCall.Intent.SkillId;
				}
			}
			return View(model);
		}

		[HttpPost]
		public ActionResult Edit(IntentCallEditViewModel model)
		{
			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				if(model.IntentCallId != 0)
				{
					IntentCall message = db.IntentCalls.FirstOrDefault(x => x.Id == model.IntentCallId);
					if (message != null)
					{
						message.CallText = model.CallText;
					}
				}
				else
				{
					IntentCall intentCall = new IntentCall()
					{
						CallText = model.CallText,
						IntentId = model.IntentId
					};
					db.IntentCalls.Add(intentCall);
				}
				db.SaveChanges();

			}
			return RedirectToAction("Detail", "Intent", new { id = model.IntentId });
		}
		#endregion

		#region Delete
		[HttpGet]
		public ActionResult DeleteConfirm(int id)
		{
			IntentCallEditViewModel model = new IntentCallEditViewModel();

			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				IntentCall intentCall = db.IntentCalls.FirstOrDefault(x => x.Id == id);
				if (intentCall != null)
				{
					model.IntentCallId = intentCall.Id;
					model.CallText = intentCall.CallText;
					model.IntentId = intentCall.IntentId;
					model.SkillId = intentCall.Intent.SkillId;
				}
			}
			return View(model);
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			IntentCallEditViewModel model = new IntentCallEditViewModel();

			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				IntentCall intentCall = db.IntentCalls.FirstOrDefault(x => x.Id == id);
				db.IntentCalls.Remove(intentCall);
				db.SaveChanges();
				return RedirectToAction("Detail", "Intent", new { id = intentCall.IntentId });
			}
			
		}
		#endregion
	}
}