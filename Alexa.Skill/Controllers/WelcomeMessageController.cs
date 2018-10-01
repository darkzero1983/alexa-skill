using Alexa.Skill.Context;
using Alexa.Skill.Models.Messages;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Alexa.Skill.Controllers
{
	public class WelcomeMessageController : Controller
	{
		#region Add
		[HttpGet]
		public ActionResult Add(int id)
		{
			WelcomeMessageEditViewModel model = new WelcomeMessageEditViewModel()
			{
				SkillId = id
			};
			return View("Edit", model);
		}
		#endregion

		#region Edit
		[HttpGet]
		public ActionResult Edit(int id)
		{
			WelcomeMessageEditViewModel model = new WelcomeMessageEditViewModel();

			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				WelcomeMessage message = db.WelcomeMessages.FirstOrDefault(x => x.Id == id);
				if (message != null)
				{
					model.MessageId = message.Id;
					model.Message = message.Message;
					model.SkillId = message.SkillId;
				}
			}
			return View(model);
		}

		[HttpPost]
		public ActionResult Edit(WelcomeMessageEditViewModel model)
		{
			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				if(model.MessageId != 0)
				{
					WelcomeMessage message = db.WelcomeMessages.FirstOrDefault(x => x.Id == model.MessageId);
					if (message != null)
					{
						message.Message = model.Message;
					}
				}
				else
				{
					WelcomeMessage message = new WelcomeMessage()
					{
						Message = model.Message,
						SkillId = model.SkillId
					};
					db.WelcomeMessages.Add(message);
				}
				db.SaveChanges();

			}
			return RedirectToAction("WelcomeMessages", "Skill", new { id = model.SkillId });
		}
		#endregion

		#region Delete
		[HttpGet]
		public ActionResult DeleteConfirm(int id)
		{
			WelcomeMessageEditViewModel model = new WelcomeMessageEditViewModel();

			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				WelcomeMessage message = db.WelcomeMessages.FirstOrDefault(x => x.Id == id);
				if (message != null)
				{
					model.MessageId = message.Id;
					model.Message = message.Message;
					model.SkillId = message.SkillId;
				}
			}
			return View(model);
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			WelcomeMessageEditViewModel model = new WelcomeMessageEditViewModel();

			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				WelcomeMessage message = db.WelcomeMessages.FirstOrDefault(x => x.Id == id);
				List<WelcomeMessagesRead> reads = db.WelcomeMessagesReads.Where(x => x.WelcomeMessageId == id).ToList();
				db.WelcomeMessagesReads.RemoveRange(reads);
				db.WelcomeMessages.Remove(message);
				db.SaveChanges();
				return RedirectToAction("WelcomeMessages", "Skill", new { id = message.SkillId });
			}
			
		}
		#endregion
	}
}