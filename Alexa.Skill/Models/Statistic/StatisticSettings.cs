using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alexa.Skill.Models.Statistic
{
	public class StatisticSettings
	{
		[Display(Name = "Anzeigen")]
		public int RequestCounts { get; set; }

		public List<SelectListItem> RequestCountOptions { get; set; }

		public StatisticSettings()
		{
			RequestCountOptions = new List<SelectListItem>() {
				new SelectListItem() { Value = "5", Text = "5" },
				new SelectListItem() { Value = "20", Text = "20" },
				new SelectListItem() { Value = "50", Text = "50" },
				new SelectListItem() { Value = "100", Text = "100" },
				new SelectListItem() { Value = "500", Text = "500" },
				new SelectListItem() { Value = "1000", Text = "1000" }
			};

		}
	}
}