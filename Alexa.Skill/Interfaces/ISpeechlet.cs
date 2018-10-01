using AlexaSkillsKit.Speechlet;

namespace Alexa.Skill.Interfaces
{
	public interface ISpeechlet
	{
		SpeechletResponse OnIntent(IntentRequest intentRequest, Session session);
		SpeechletResponse OnLaunch(LaunchRequest launchRequest, Session session);
		void OnSessionStarted(SessionStartedRequest sessionStartedRequest, Session session);
		void OnSessionEnded(SessionEndedRequest sessionEndedRequest, Session session);
	}
}
