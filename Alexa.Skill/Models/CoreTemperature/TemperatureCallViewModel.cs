using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alexa.Skill.Models.CoreTemperature
{
	public class TemperatureCallViewModel
	{
		public int SkillId { get; set; }
		public int TemperatureId { get; set; }

		[AllowHtml]
		public string TemperatureName { get; set; }

		public List<TemperatureCallItemViewModel> Calls { get; set; }

		[Display(Name = "Name")]
		[AllowHtml]
		public string AddCallName { get; set; }

		public TemperatureCallViewModel()
		{
			Calls = new List<TemperatureCallItemViewModel>();
		}
	}
}