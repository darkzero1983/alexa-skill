using Alexa.Skill.Models.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alexa.Skill.Models.Intents
{
	public class IntentDetailViewModel
	{
		public int IntentId { get; set; }
		public string Name { get; set; }
		public int SkillId { get; set; }
		public string SkillName { get; set; }

		public List<MessageOverviewItemViewModel> Messages { get; set; }

		public Dictionary<int, string> IntentCalls { get; set; }

		public IntentDetailViewModel()
		{
			Messages = new List<MessageOverviewItemViewModel>();
			IntentCalls = new Dictionary<int, string>();
		}
	}
}