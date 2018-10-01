using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Alexa.Skill.Models.CoreTemperature
{
	public class UserCallOverviewViewModel
	{
		public int SkillId { get; set; }
		public List<UserCallViewModel> UserCalls { get; set; }

		[Display(Name = "Antwort")]
		public int? AnswerId { get; set; }
		public IEnumerable<SelectListItem> Answers { get; set; }

		[Display(Name = "Kerntemperatur")]
		public int? CoreTemperatureId { get; set; }
		public IEnumerable<SelectListItem> CoreTemperatures { get; set; }
		
		public UserCallOverviewViewModel()
		{
			UserCalls = new List<UserCallViewModel>();
			Answers = new List<SelectListItem>();
			CoreTemperatures = new List<SelectListItem>();
		}
	}
}