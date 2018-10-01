using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alexa.Skill.Models.Intents
{
	public class IntentOverviewViewModel
	{
		public int SkillId { get; set; }
		public List<IntentItemViewModel> Intents { get; set; }

		public IntentOverviewViewModel()
		{
			Intents = new List<IntentItemViewModel>();
		}
	}
}