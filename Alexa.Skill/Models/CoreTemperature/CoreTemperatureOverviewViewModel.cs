using System.Collections.Generic;

namespace Alexa.Skill.Models.CoreTemperature
{
	public class CoreTemperatureOverviewViewModel
	{
		public int SkillId { get; set; }
				
		public List<CoreTemperatureOverviewItemViewModel> CoreTemperatures { get; set; }

		public CoreTemperatureOverviewViewModel()
		{
			CoreTemperatures = new List<CoreTemperatureOverviewItemViewModel>();
		}
	}
}