using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alexa.Skill.Models
{
	public class FlashBriefingResult
	{
		public string uid { get; set; }
		public string updateDate { get; set; }
		public string titleText { get; set; }
		public string mainText { get; set; }
		public string redirectionUrl { get; set; }

	}
}