using System.Web.Mvc;

namespace Alexa.Skill.Models.CoreTemperature
{
	public class AnswerOverviewItemViewModel
	{
		public int Id { get; set; }
		[AllowHtml]
		public string Answer { get; set; }
	}
}