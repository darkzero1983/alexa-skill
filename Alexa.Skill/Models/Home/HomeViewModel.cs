using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alexa.Skill.Models.Home
{
	public class HomeViewModel
	{
		public List<SkillOverviewItem> Skills { get; set; }

		public HomeViewModel()
		{
			Skills = new List<SkillOverviewItem>();
		}
	}
}