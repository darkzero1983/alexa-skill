using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alexa.Skill.Models.Statistic
{
	public class RequestLogViewModel
	{
		public System.DateTime RequestTime { get; set; }
		public string UserId { get; set; }
		public string RequestType { get; set; }
		public string Intent { get; set; }
		public string IntentParam { get; set; }
		public string Information { get; set; }
	}
}