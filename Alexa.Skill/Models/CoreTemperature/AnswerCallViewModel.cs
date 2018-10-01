using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alexa.Skill.Models.CoreTemperature
{
	public class AnswerCallViewModel
	{
		public int SkillId { get; set; }
		public int AnswerId { get; set; }
		[AllowHtml]
		public string Answer { get; set; }

		public List<AnswerCallItemViewModel> Calls { get; set; }

		[Display(Name = "Name")]
		[AllowHtml]
		public string AddCallName { get; set; }

		public AnswerCallViewModel()
		{
			Calls = new List<AnswerCallItemViewModel>();
		}
	}
}