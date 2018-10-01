using Alexa.Skill.Models;
using Alexa.Skill.SkillSpechlets;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Alexa.Skill.Api
{
	public class FlashBriefingSkillController : ApiController
	{
		[Route("flashbriefing-skill")]
		[HttpGet]
		public List<FlashBriefingResult> AlexaRequest()
		{
			List<FlashBriefingResult> result = new List<FlashBriefingResult>();
			result.Add(new FlashBriefingResult()
			{
				uid = "EXAMPLE_CHANNEL_MULTI_ITEM_JSON_TTS_1",
				updateDate = "2017-08-13T00:00:00.0Z",
				titleText = "Meine erste Überschrift",
				mainText = "Das ist der Inhalt meiner ersten Nachricht. Diese ist natürlich etwas kurz.",
				redirectionUrl = "https://www.amazon.com"
			});
			return result;
		}
	}
}