using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alexa.Skill.Models.Intents
{
	public class IntentItemViewModel
	{
		public int Id { get; set; }

		[AllowHtml]
		public string Name { get; set; }
	}
}