using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alexa.Skill.Models.Messages
{
	public class IntentCallEditViewModel
	{
		public int IntentId { get; set; }
		public int SkillId { get; set; }
		public int IntentCallId { get; set; }
		public string CallText { get; set; }
	}
}