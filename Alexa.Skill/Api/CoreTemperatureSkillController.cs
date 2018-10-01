using Alexa.Skill.SkillSpechlets;
using System.Net.Http;
using System.Web.Http;

namespace Alexa.Skill.Api
{
	public class CoreTemperatureSkillController : ApiController
	{
		[Route("coretemperature-skill")]
		[HttpPost]
		public HttpResponseMessage AlexaRequest()
		{
			CoreTemperatureSkillSpeechlet speechlet = new CoreTemperatureSkillSpeechlet();
			return speechlet.GetResponse(Request);
		}
	}
}