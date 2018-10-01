using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alexa.Skill.Models.Messages
{
	public class WelcomeMessageEditViewModel
	{
		public int SkillId { get; set; }
		public int MessageId { get; set; }
		[AllowHtml]
		public string Message { get; set; }
	}
}