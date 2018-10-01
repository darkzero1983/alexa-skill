using AlexaSkillsKit.Speechlet;
using System.Threading.Tasks;

namespace Alexa.Skill.Interfaces
{
	public interface ISpeechletAsync
	{
		Task<SpeechletResponse> OnIntentAsync(IntentRequest intentRequest, Session session);
		Task<SpeechletResponse> OnLaunchAsync(LaunchRequest launchRequest, Session session);
		Task OnSessionStartedAsync(SessionStartedRequest sessionStartedRequest, Session session);
		Task OnSessionEndedAsync(SessionEndedRequest sessionEndedRequest, Session session);
	}
}
