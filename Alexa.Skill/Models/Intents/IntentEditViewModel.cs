using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Alexa.Skill.Models.Intents
{
	public class IntentEditViewModel
	{
		[Display(Name = "Session beenden?")]
		public bool ShouldEndSession { get; set; }
		public int IntentId { get; set; }
		[Display(Name = "Intent Name")]
		public string Name { get; set; }

		public int SkillId { get; set; }
		public IntentEditViewModel()
		{
			ShouldEndSession = true;
		}
	}
}