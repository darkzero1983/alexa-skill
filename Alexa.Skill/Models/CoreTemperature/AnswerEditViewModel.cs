using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alexa.Skill.Models.CoreTemperature
{
	public class AnswerEditViewModel
	{
		public int SkillId { get; set; }

		public int? AnswereId { get; set; }

		[AllowHtml]
		public string Answer { get; set; }
		[AllowHtml]
		public string SessionTerm { get; set; }

		public bool ShouldEndSession { get; set; }
	}
}