using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alexa.Skill.Models
{
	public class IntentMessageModel
	{
		public string Mesasge { get; set; }
		public bool ShouldEndSession { get; set; }
	}
}