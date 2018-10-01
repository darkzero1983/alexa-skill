using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alexa.Skill.Models.CoreTemperature
{
	public class TemperatureEditViewModel
	{
		public int SkillId { get; set; }

		public int? TemperatureId { get; set; }
		[AllowHtml]
		public string Name { get; set; }

		public Nullable<int> DefaultDegree { get; set; }
		public Nullable<int> DefaultMinDegree { get; set; }
		public Nullable<int> DefaultMaxDegree { get; set; }
		public Nullable<int> RareDegree { get; set; }
		public Nullable<int> RareMinDegree { get; set; }
		public Nullable<int> RareMaxDegree { get; set; }
		public Nullable<int> MediumDegree { get; set; }
		public Nullable<int> MediumMinDegree { get; set; }
		public Nullable<int> MediumMaxDegree { get; set; }
		public Nullable<int> DoneDegree { get; set; }
		public Nullable<int> DoneMinDegree { get; set; }
		public Nullable<int> DoneMaxDegree { get; set; }
	}
}