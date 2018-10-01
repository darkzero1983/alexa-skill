using Alexa.Skill.SkillSpechlets;
using System.Net.Http;
using System.Web.Http;

namespace Alexa.Skill.Api
{
	public class PeterSkillController : ApiController
	{
		[Route("peter-skill")]
		[HttpPost]
		public HttpResponseMessage AlexaRequest()
		{
			PeterSkillSpeechlet speechlet = new PeterSkillSpeechlet();
			return speechlet.GetResponse(Request);
		}
	}
}