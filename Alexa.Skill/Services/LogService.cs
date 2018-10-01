using Alexa.Skill.Context;
using Alexa.Skill.Enums;
using AlexaSkillsKit.Slu;
using AlexaSkillsKit.Speechlet;
using System;
using System.Linq;

namespace Alexa.Skill.Services
{
	public class LogService
	{
		public void LogRequest(LaunchRequest request, Session session, string resultMessage)
		{
			if(request == null)
			{
				return;
			}
			LogRequestToDb(AlexaRequestType.LaunchRequest, session.Application.Id, session.User.Id, null, null, resultMessage);
		}

		public void LogRequest(SessionStartedRequest request, Session session, string resultMessage)
		{
			//LogRequestToDb(AlexaRequestType.SessionStartedRequest, session.Application.Id, session.User.Id, null);
		}

		public void LogRequest(IntentRequest request, Session session, string resultMessage)
		{
			if(request == null)
			{
				return;
			}

			AlexaSkillsKit.Slu.Intent intent = request.Intent;
			string intentName = (intent != null) ? intent.Name : null;
			string intentParam = null;
			string delimeter = "";
			if (request.Intent != null && request.Intent.Slots != null && request.Intent.Slots.Count > 0)
			{
				intentParam = "";
				foreach (var slot in request.Intent.Slots)
				{
					intentParam = intentParam + delimeter + slot.Value.Value;
					delimeter = " | ";
				}
			}
			
			if (session.Attributes.ContainsKey("sessionIntentParam"))
			{
				intentParam = intentParam + delimeter + "session: " + session.Attributes["sessionIntentParam"] + "";
			}
			LogRequestToDb(AlexaRequestType.IntentRequest, session.Application.Id, session.User.Id, intentName, intentParam, resultMessage);
		}


		public void LogRequest(SessionEndedRequest request, Session session, string resultMessage)
		{
			//LogRequestToDb(AlexaRequestType.SessionEndedRequest, session.Application.Id, session.User.Id, null);
		}

		private void LogRequestToDb(AlexaRequestType requestType, string applicationId, string userId, string intent, string intentParam, string resultMessage)
		{
			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				db.RequestLogs.Add(new RequestLog() {
					RequestTime = DateTime.Now,
					ApplicationId = applicationId,
					UserId = userId,
					RequestType = requestType.ToString(),
					Intent = intent,
					IntentParam = intentParam,
					Information = resultMessage
				});
				db.SaveChanges();
			}
		}
	}
}