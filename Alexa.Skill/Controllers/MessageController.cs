using Alexa.Skill.Context;
using Alexa.Skill.Models.Messages;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Alexa.Skill.Controllers
{
	public class MessageController : Controller
	{
		#region Add
		[HttpGet]
		public ActionResult Add(int id)
		{
			MessageEditViewModel model = new MessageEditViewModel()
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
			MessageEditViewModel model = new MessageEditViewModel();

			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				IntentMessage message = db.IntentMessages.FirstOrDefault(x => x.Id == id);
				if (message != null)
				{
					model.MessageId = message.Id;
					model.Message = message.Message;
					model.IntentId = message.IntentId;
					model.SkillId = message.Intent.SkillId;
				}
			}
			return View(model);
		}

		[HttpPost]
		public ActionResult Edit(MessageEditViewModel model)
		{
			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				if(model.MessageId != 0)
				{
					IntentMessage message = db.IntentMessages.FirstOrDefault(x => x.Id == model.MessageId);
					if (message != null)
					{
						message.Message = model.Message;
					}
				}
				else
				{
					IntentMessage message = new IntentMessage()
					{
						Message = model.Message,
						IntentId = model.IntentId
					};
					db.IntentMessages.Add(message);
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
			MessageEditViewModel model = new MessageEditViewModel();

			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				IntentMessage message = db.IntentMessages.FirstOrDefault(x => x.Id == id);
				if (message != null)
				{
					model.MessageId = message.Id;
					model.Message = message.Message;
					model.IntentId = message.IntentId;
					model.SkillId = message.Intent.SkillId;
				}
			}
			return View(model);
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			MessageEditViewModel model = new MessageEditViewModel();

			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				IntentMessage message = db.IntentMessages.FirstOrDefault(x => x.Id == id);
				List<IntentMessagesRead> reads = db.IntentMessagesReads.Where(x => x.IntentMessageId == id).ToList();
				db.IntentMessagesReads.RemoveRange(reads);
				db.IntentMessages.Remove(message);
				db.SaveChanges();
				return RedirectToAction("Detail", "Intent", new { id = message.IntentId });
			}
			
		}
		#endregion
	}
}