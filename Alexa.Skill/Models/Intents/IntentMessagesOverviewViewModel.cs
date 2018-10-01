using Alexa.Skill.Models.Messages;
using System.Collections.Generic;

namespace Alexa.Skill.Models.Intents
{
	public class IntentMessagesOverviewViewModel
	{
		public int IntentId { get; set; }
		public string IntentName { get; set; }
		public string SkillName { get; set; }

		public List<MessageOverviewItemViewModel> Messages { get; set; }

		public IntentMessagesOverviewViewModel()
		{
			Messages = new List<MessageOverviewItemViewModel>();
		}
	}
}