using Alexa.Skill.Models.Messages;
using System.Collections.Generic;

namespace Alexa.Skill.Models.Intents
{
	public class SkillWelcomeMessagesOverviewViewModel
	{
		public int SkillId { get; set; }

		public List<MessageOverviewItemViewModel> Messages { get; set; }

		public SkillWelcomeMessagesOverviewViewModel()
		{
			Messages = new List<MessageOverviewItemViewModel>();
		}
	}
}