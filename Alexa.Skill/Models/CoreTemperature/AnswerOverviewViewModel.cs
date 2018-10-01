using System.Collections.Generic;

namespace Alexa.Skill.Models.CoreTemperature
{
	public class AnswerOverviewViewModel
	{
		public int SkillId { get; set; }
				
		public List<AnswerOverviewItemViewModel> Answers { get; set; }

		public AnswerOverviewViewModel()
		{
			Answers = new List<AnswerOverviewItemViewModel>();
		}
	}
}