using System.Web.Mvc;

namespace Alexa.Skill.Models.CoreTemperature
{
	public class UserCallViewModel
	{
		public int UserCallId { get; set; }
		[AllowHtml]
		public string UserCallName { get; set; }

		public int? CoreTemperatureId { get; set; }
		[AllowHtml]
		public string CoreTemeratureName { get; set; }

		public int? AnswerId { get; set; }
		[AllowHtml]
		public string AnswerText { get; set; }
	}
}