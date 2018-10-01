using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alexa.Skill.Models.Home
{
	public class SkillOverviewItem
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public int UsageCount { get; set; }
		public int UserCount { get; set; }

		public int UsageCount24h { get; set; }
		public int UserCount24h { get; set; }
	}
}