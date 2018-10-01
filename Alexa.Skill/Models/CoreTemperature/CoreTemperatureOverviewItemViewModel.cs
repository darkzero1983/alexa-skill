using System.Web.Mvc;

namespace Alexa.Skill.Models.CoreTemperature
{
	public class CoreTemperatureOverviewItemViewModel
	{
		public int Id { get; set; }
		[AllowHtml]
		public string Name { get; set; }
	}
}