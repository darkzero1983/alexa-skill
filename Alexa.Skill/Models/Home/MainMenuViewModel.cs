using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alexa.Skill.Models.Home
{
	public class MainMenuViewModel
	{
		public int SkillId { get; set; }

		public Dictionary<int,string> Intents { get; set; }

		public MainMenuViewModel()
		{
			Intents = new Dictionary<int, string>();
		}
	}
}