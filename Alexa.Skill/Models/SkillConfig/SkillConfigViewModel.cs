using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alexa.Skill.Models.SkillConfig
{
	public class SkillConfigViewModel
	{
		public int SkillId { get; set; }
		public string IntentSchema { get; set; }
		public string SampleUtterances { get; set; }
	}
}