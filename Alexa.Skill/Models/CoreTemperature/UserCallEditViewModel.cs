using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Alexa.Skill.Models.CoreTemperature
{
	public class UserCallEditViewModel
	{
		public int SkillId { get; set; }
		public int UserCallId { get; set; }

		[AllowHtml]
		[Display(Name = "User Aufruf")]
		public string UserCallName { get; set; }

		[Display(Name = "Antwort")]
		public int? AnswerId { get; set; }
		public IEnumerable<SelectListItem> Answers { get; set; }

		[Display(Name = "Kerntemperatur")]
		public int? CoreTemperatureId { get; set; }
		public IEnumerable<SelectListItem> CoreTemperatures { get; set; }

		public UserCallEditViewModel()
		{
			Answers = new List<SelectListItem>();
			CoreTemperatures = new List<SelectListItem>();
		}
	}
}