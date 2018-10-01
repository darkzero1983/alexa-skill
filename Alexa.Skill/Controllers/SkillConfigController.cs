using Alexa.Skill.Context;
using Alexa.Skill.Models.SkillConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alexa.Skill.Controllers
{
	public class SkillConfigController : Controller
	{
		public ActionResult Index(int id)
		{
			SkillConfigViewModel model = new SkillConfigViewModel()
			{
				SkillId = id
			};
			string intents = "";
			string delimiter = "";
			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				Context.Skill skill = db.Skills.FirstOrDefault(x => x.Id == id);
				if(skill == null)
				{
					return null;
				}
				foreach (Intent intent in skill.Intents.Where(x => x.IntentCalls.Count > 0))
				{
					intents = intents + delimiter + "{\"intent\": \"" + intent.Name + "\"}";
					delimiter = ",";
					foreach (IntentCall intentCall in intent.IntentCalls)
					{
						model.SampleUtterances = model.SampleUtterances + intent.Name + " " + intentCall.CallText + Environment.NewLine;
					};
					
				}
			}
			model.IntentSchema = "{\"intents\": [" + intents + "]}";

			return View(model);
		}
	}
}