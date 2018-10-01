using Alexa.Skill.Models;
using Alexa.Skill.Services;
using AlexaSkillsKit.Authentication;
using AlexaSkillsKit.Json;
using AlexaSkillsKit.Slu;
using AlexaSkillsKit.Speechlet;
using AlexaSkillsKit.UI;
using System;
using System.Collections.Generic;

namespace Alexa.Skill.SkillSpechlets
{
	public class InformationSkillSpeechlet : Speechlet
	{
		private InformationSkillService _informationSkillSerive;
		private LogService _logService;

		public InformationSkillSpeechlet()
		{
			_informationSkillSerive = new InformationSkillService();
			_logService = new LogService();
		}

		public override SpeechletResponse OnLaunch(LaunchRequest request, Session session)
		{
			string SkillName = _informationSkillSerive.GetSkillName(session.Application.Id);
			string speechOutput;
			bool shouldSessionEnd = false;
			try
			{ 
				speechOutput = _informationSkillSerive.GetWelcomeMessage(session.Application.Id);
				if(String.IsNullOrWhiteSpace(speechOutput))
				{
					IntentMessageModel intentMessageModel = _informationSkillSerive.GetAnyIntentMessage(session.Application.Id, session.User.Id);
					if(intentMessageModel != null)
					{
						speechOutput = intentMessageModel.Mesasge;
						shouldSessionEnd = intentMessageModel.ShouldEndSession;
					}
				}
				
			}
			catch(Exception e)
			{
				speechOutput = e.Message;
			}
			if (speechOutput == null)
			{
				speechOutput = "Willkommen";
			}

			return BuildSpeechletResponse(SkillName, speechOutput, shouldSessionEnd, request, null, session);
		}


		// Note: NAME_KEY being a JSON property key gets camelCased during serialization
		private const string NAME_KEY = "name";
		private const string NAME_SLOT = "Name";


		public override void OnSessionStarted(SessionStartedRequest request, Session session)
		{
			//_logService.LogRequest(request, session);
		}


		

		public override SpeechletResponse OnIntent(IntentRequest request, Session session)
		{
			string SkillName = _informationSkillSerive.GetSkillName(session.Application.Id);
			Intent intent = request.Intent;
			string intentName = (intent != null) ? intent.Name : null;

			IntentMessageModel speechOutput = _informationSkillSerive.GetIntentMessage(session.Application.Id, intentName, session.User.Id);
			if(speechOutput == null || String.IsNullOrWhiteSpace(speechOutput.Mesasge))
			{
				return BuildSpeechletResponse(intent.Name, "", true, null, request, session);
			}
			return BuildSpeechletResponse(SkillName, speechOutput.Mesasge, speechOutput.ShouldEndSession,  null, request, session);
		}


		public override void OnSessionEnded(SessionEndedRequest request, Session session)
		{
			//_logService.LogRequest(request, session);
		}
		
		
		private SpeechletResponse BuildSpeechletResponse(string title, string output, bool shouldEndSession, LaunchRequest launchRequest, IntentRequest intentRequest, Session session)
		{
			_logService.LogRequest(launchRequest, session, output);
			_logService.LogRequest(intentRequest, session, output);

			// Create the Simple card content.
			SimpleCard card = new SimpleCard()
			{
				Title = title,
				Content = output
			};

			// Create the plain text output.
			SsmlOutputSpeech speech = new SsmlOutputSpeech();
			speech.Ssml = "<speak>" + output + " </speak>";

			// Create the speechlet response.
			SpeechletResponse response = new SpeechletResponse();
			response.ShouldEndSession = shouldEndSession;
			response.OutputSpeech = speech;
			if(String.IsNullOrWhiteSpace(output))
			{
				//response.Card = card;
			}
			
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