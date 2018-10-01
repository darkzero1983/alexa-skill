using Alexa.Skill.Context;
using Alexa.Skill.Services;
using AlexaSkillsKit.Authentication;
using AlexaSkillsKit.Json;
using AlexaSkillsKit.Speechlet;
using AlexaSkillsKit.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alexa.Skill.SkillSpechlets
{
	public class PeterSkillSpeechlet : Speechlet
	{
		private LogService _logService;

		public PeterSkillSpeechlet()
		{
			_logService = new LogService();
		}
		
		public override SpeechletResponse OnLaunch(LaunchRequest request, Session session)
		{
			string speechOutput;
			bool shouldSessionEnd = false;

			speechOutput = "Hi, ich bins Peter Ludwig, was kann ich für dich tun?";

			return BuildSpeechletResponse("Kerntemperaturen", speechOutput, shouldSessionEnd, request, null, session);
		}


		// Note: NAME_KEY being a JSON property key gets camelCased during serialization
		private const string NAME_KEY = "name";
		private const string NAME_SLOT = "Name";


		public override void OnSessionStarted(SessionStartedRequest request, Session session)
		{
			
		}


		

		public override SpeechletResponse OnIntent(IntentRequest request, Session session)
		{
			AlexaSkillsKit.Slu.Intent intent = request.Intent;

			string intentName = (intent != null) ? intent.Name : null;

			switch (intentName)
			{
				case "Exit":
					return BuildSpeechletResponse("Peter Ludwig", "Dann machs gut", true, null, request, session);
				case "PeterWhoLove":
					return BuildSpeechletResponse("Peter Ludwig", "Natürlich Caro, die schönste und tollste Frau auf der ganzen Welt", true, null, request, session);
				case "PeterWhenMarried":
					return BuildSpeechletResponse("Peter Ludwig", "Ich habe am 16.08.2016 geheiratet. Das war der schönste Tag in meinem Leben.", true, null, request, session);
				case "SkillStatus":
					string skillStatus = "";
					using (AlexaSkillEntities db = new AlexaSkillEntities())
					{
						List<Context.Skill> skills = db.Skills.ToList();
						skillStatus = "Ich habe aktuell " + skills.Count() + " Skills.,,,,,";

						List<Context.RequestLog> logs = db.RequestLogs.ToList();
						skillStatus = skillStatus + "Alle Skills zusammen, wurden insgesamt, " + logs.Count() + " mal, von " + logs.Select(x => x.UserId).Distinct().Count() + " verschiedenen User, aufgerufen., Nun zu den einzelnen Skills.,,,,,";

						foreach (Context.Skill skill in skills)
						{
							List<Context.RequestLog> skillLog = logs.Where(x => x.ApplicationId == skill.ApplicationID).ToList();

							skillStatus = skillStatus + " Der Skill " + skill.Name + " wurde insgesamt " + skillLog.Count() + " mal, von " + skillLog.Select(x => x.UserId).Distinct().Count() + " verschiedenen Usern, aufgerufen.,,,,,";
						}
					}
					return BuildSpeechletResponse("Peter Ludwig", skillStatus, true, null, request, session);

			}

			return BuildSpeechletResponse("Peter Ludwig", "Ich weis nicht was du meinst.", true, null, request, session);
		}


		public override void OnSessionEnded(SessionEndedRequest request, Session session)
		{
			
		}

		private SpeechletResponse BuildSpeechletResponse(string title, string output, bool shouldEndSession, LaunchRequest launchRequest, IntentRequest intentRequest, Session session)
		{
			_logService.LogRequest(intentRequest, session, output);

			// Create the Simple card content.
			SimpleCard card = new SimpleCard();
			card.Title = title;
			card.Content = output;
			
			SsmlOutputSpeech ssml = new SsmlOutputSpeech()
			{
				Ssml = "<speak>" + output + " </speak>"
			};

			// Create the speechlet response.
			SpeechletResponse response = new SpeechletResponse()
			{
				ShouldEndSession = shouldEndSession,
				OutputSpeech = ssml,
				Card = card
			};
			
			//response.Card = card;
			return response;
		}

		#if DEBUG
		public override bool OnRequestValidation(SpeechletRequestValidationResult result, DateTime referenceTimeUtc, SpeechletRequestEnvelope requestEnvelope)
		{
			return true;
			if (result != SpeechletRequestValidationResult.OK)
			{
				if (result.HasFlag(SpeechletRequestValidationResult.NoSignatureHeader))
				{
					//Debug.WriteLine("Alexa request is missing signature header, rejecting.");
					return false;
				}
				if (result.HasFlag(SpeechletRequestValidationResult.NoCertHeader))
				{
					//Debug.WriteLine("Alexa request is missing certificate header, rejecting.");
					return false;
				}
				if (result.HasFlag(SpeechletRequestValidationResult.InvalidSignature))
				{
					//Debug.WriteLine("Alexa request signature is invalid, rejecting.");
					return false;
				}
				else
				{
					if (result.HasFlag(SpeechletRequestValidationResult.InvalidTimestamp))
					{
						var diff = referenceTimeUtc - requestEnvelope.Request.Timestamp;
						//Debug.WriteLine("Alexa request timestamped '{0:0.00}' seconds ago making timestamp invalid, but continue processing.",diff.TotalSeconds);
					}
					return true;
				}
			}
			else
			{
				var diff = referenceTimeUtc - requestEnvelope.Request.Timestamp;
				//Debug.WriteLine("Alexa request timestamped '{0:0.00}' seconds ago.", diff.TotalSeconds);
				return true;
			}
		}
#endif
	}
}