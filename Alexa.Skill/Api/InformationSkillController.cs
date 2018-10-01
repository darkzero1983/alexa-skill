using Alexa.Skill.SkillSpechlets;
using System.Net.Http;
using System.Web.Http;

namespace Alexa.Skill.Api
{
	public class InformationSkillController : ApiController
	{
		[Route("information-skill")]
		[HttpPost]
		public HttpResponseMessage AlexaRequest()
		{
			InformationSkillSpeechlet speechlet = new InformationSkillSpeechlet();
			return speechlet.GetResponse(Request);
		}
	}
}
