using Alexa.Skill.Context;
using Alexa.Skill.Services;
using AlexaSkillsKit.Authentication;
using AlexaSkillsKit.Json;
using AlexaSkillsKit.Speechlet;
using AlexaSkillsKit.UI;
using System;
using System.Linq;

namespace Alexa.Skill.SkillSpechlets
{
	public class CoreTemperatureSkillSpeechlet : Speechlet
	{
		private InformationSkillService _informationSkillSerive;
		private LogService _logService;

		public CoreTemperatureSkillSpeechlet()
		{
			_informationSkillSerive = new InformationSkillService();
			_logService = new LogService();
		}
		

		public override SpeechletResponse OnLaunch(LaunchRequest request, Session session)
		{
			string speechOutput;
			bool shouldSessionEnd = false;

			speechOutput = "Willkommen bei dem Kerntermperatur Skill für alle Grill und Koch begeisterten. Für welches Fleisch möchtest du die Kerntemperaturen wissen?";

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
			string speechOutput = null;
			AlexaSkillsKit.Slu.Intent intent = request.Intent;

			string intentName = (intent != null) ? intent.Name : null;

			switch (intentName)
			{
				case "GetTemperature":
					string meatSearchTerm = request.Intent.Slots.First().Value.Value;
					if(String.IsNullOrWhiteSpace(meatSearchTerm))
					{
						return BuildSpeechletResponse("Kerntemperaturen", "Das habe ich leider nicht verstanden", true, null, request, session);
					}

					switch(meatSearchTerm.ToLower())
					{
						case "stopp":
						case "abbrechen":
							return BuildSpeechletResponse("Kerntemperaturen", "", true, null, request, session);
					}

					switch (meatSearchTerm.ToLower())
					{
						case "hilfe":
							return BuildSpeechletResponse("Kerntemperaturen", "Du kannst hier nach Kerntemperaturen für Fleisch und Fisch fragen, sage jetzt z.B. Rinderfilet", false, null, request, session);
					}

					using (var db = new AlexaSkillEntities())
					{
						Meat meat = null;
						string sessionIntentParam = "";
						if (session.Attributes.ContainsKey("sessionIntentParam"))
						{
							sessionIntentParam = session.Attributes["sessionIntentParam"];
						}

						meat = db.Meats.FirstOrDefault(x => x.Name == sessionIntentParam + meatSearchTerm);

						if (meat == null)
						{
							meat = db.Meats.FirstOrDefault(x => x.Name == meatSearchTerm + sessionIntentParam);
						}

						if (meat == null)
						{
							meat = db.Meats.FirstOrDefault(x => x.Name == meatSearchTerm);
						}
						if (meat == null)
						{ 
							foreach (string searchterm in meatSearchTerm.Split(' '))
							{
								meat = db.Meats.FirstOrDefault(x => x.Name == searchterm);
								if(meat != null)
								{
									continue;
								}
							}
						}

						if (meat == null)
						{
							return BuildSpeechletResponse("Kerntemperaturen", "Für " + meatSearchTerm + " habe ich leider keine Informationen", true, null, request, session);
						}

						if (meat.AnswerId != null)
						{
							string sessionIntentParamValue = meatSearchTerm;

							if(!String.IsNullOrEmpty(meat.Answer.SessionTerm))
							{
								sessionIntentParamValue = meat.Answer.SessionTerm;
							}

							if (session.Attributes.ContainsKey("sessionIntentParam"))
							{
								session.Attributes["sessionIntentParam"] = sessionIntentParamValue;
							}
							else
							{
								session.Attributes.Add("sessionIntentParam", sessionIntentParamValue);
							}
							return BuildSpeechletResponse("Kerntemperaturen", meat.Answer.Answert, meat.Answer.ShouldEndSession, null, request, session);
						}

						if (meat.CoreTemperatureId != null)
						{
							string delimiter = "";
							string termperatureResult = "";

							if (meat.CoreTemperature.DefaultDegree != null)
							{
								termperatureResult = termperatureResult + delimiter + meat.CoreTemperature.DefaultDegree + " Grad";
								delimiter = ", ";
							}
							
							if (meat.CoreTemperature.DefaultMinDegree != null && meat.CoreTemperature.DefaultMaxDegree != null)
							{
								termperatureResult = termperatureResult + delimiter + " zwischen " + meat.CoreTemperature.DefaultMinDegree + " und " + meat.CoreTemperature.DefaultMaxDegree + " Grad";
								delimiter = ", ";
							}
							else if (meat.CoreTemperature.DefaultMinDegree != null)
							{
								termperatureResult = termperatureResult + delimiter + " ab " + meat.CoreTemperature.DefaultMinDegree + " Grad";
								delimiter = ", ";
							}
							else if (meat.CoreTemperature.DefaultMaxDegree != null)
							{
								termperatureResult = termperatureResult + delimiter + " bis " + meat.CoreTemperature.DefaultMaxDegree + " Grad";
								delimiter = ", ";
							}

							if (meat.CoreTemperature.RareDegree != null)
							{
								termperatureResult = termperatureResult + delimiter + " für Blutig, " + meat.CoreTemperature.RareDegree + " Grad";
								delimiter = ", ";
							}

							if (meat.CoreTemperature.RareMinDegree != null && meat.CoreTemperature.RareMaxDegree != null)
							{
								termperatureResult = termperatureResult + delimiter + " für Blutig, zwischen " + meat.CoreTemperature.RareMinDegree + " und " + meat.CoreTemperature.RareMaxDegree + " Grad";
								delimiter = ", ";
							}
							else if (meat.CoreTemperature.RareMinDegree != null)
							{
								termperatureResult = termperatureResult + delimiter + " für Blutig, ab " + meat.CoreTemperature.RareMinDegree + " Grad";
								delimiter = ", ";
							}
							else if (meat.CoreTemperature.RareMaxDegree != null)
							{
								termperatureResult = termperatureResult + delimiter + " für Blutig, bis " + meat.CoreTemperature.RareMaxDegree + " Grad";
								delimiter = ", ";
							}

							if (meat.CoreTemperature.MediumDegree != null)
							{
								termperatureResult = termperatureResult + delimiter + " für Medium, " + meat.CoreTemperature.MediumDegree + " Grad";
								delimiter = ", ";
							}

							if (meat.CoreTemperature.MediumMinDegree != null && meat.CoreTemperature.MediumMaxDegree != null)
							{
								termperatureResult = termperatureResult + delimiter + " für Medium, zwischen " + meat.CoreTemperature.MediumMinDegree + " und " + meat.CoreTemperature.MediumMaxDegree + " Grad";
								delimiter = ", ";
							}
							else if (meat.CoreTemperature.MediumMinDegree != null)
							{
								termperatureResult = termperatureResult + delimiter + " für Medium, ab " + meat.CoreTemperature.MediumMinDegree + " Grad";
								delimiter = ", ";
							}
							else if (meat.CoreTemperature.MediumMaxDegree != null)
							{
								termperatureResult = termperatureResult + delimiter + " für Medium, bis " + meat.CoreTemperature.MediumMaxDegree + " Grad";
								delimiter = ", ";
							}

							if (meat.CoreTemperature.DoneDegree != null)
							{
								termperatureResult = termperatureResult + delimiter + " für Durch, " + meat.CoreTemperature.DoneDegree + " Grad";
								delimiter = ", ";
							}

							if (meat.CoreTemperature.DoneMinDegree != null && meat.CoreTemperature.DoneMaxDegree != null)
							{
								termperatureResult = termperatureResult + delimiter + " für Durch, zwischen " + meat.CoreTemperature.DoneMinDegree + " und " + meat.CoreTemperature.DoneMaxDegree + " Grad";
								delimiter = ", ";
							}
							else if (meat.CoreTemperature.DoneMinDegree != null)
							{
								termperatureResult = termperatureResult + delimiter + " für Durch, ab " + meat.CoreTemperature.DoneMinDegree + " Grad";
								delimiter = ", ";
							}
							else if (meat.CoreTemperature.DoneMaxDegree != null)
							{
								termperatureResult = termperatureResult + delimiter + " für Durch, bis " + meat.CoreTemperature.DoneMaxDegree + " Grad";
								delimiter = ", ";
							}


							return BuildSpeechletResponse("Kerntemperaturen", "Die Kerntemperatur für " + meat.Name + " beträgt " + termperatureResult, true, null, request, session);
						}


						break;
					}
			}

			return BuildSpeechletResponse(intent.Name, speechOutput, true, null, request, session);
		}


		public override void OnSessionEnded(SessionEndedRequest request, Session session)
		{
			
		}

		private SpeechletResponse BuildSpeechletResponse(string title, string output, bool shouldEndSession, LaunchRequest launchRequest, IntentRequest intentRequest, Session session)
		{
			_logService.LogRequest(launchRequest, session, output);
			_logService.LogRequest(intentRequest, session, output);

			// Create the Simple card content.
			SimpleCard card = new SimpleCard();
			card.Title = title;
			card.Content = output;

			// Create the plain text output.
			SsmlOutputSpeech speech = new SsmlOutputSpeech()
			{
				Ssml = "<speak>" + output + " </speak>",
			};

			// Create the speechlet response.
			SpeechletResponse response = new SpeechletResponse()
			{
				ShouldEndSession = shouldEndSession,
				OutputSpeech = speech,
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