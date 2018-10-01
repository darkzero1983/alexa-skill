using Alexa.Skill.Context;
using Alexa.Skill.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alexa.Skill.Services
{
	public class InformationSkillService
	{
		private int GetSkillId(string applicationId)
		{
			int skillId = 0;
			using (var db = new AlexaSkillEntities())
			{
				Context.Skill skill = db.Skills.FirstOrDefault(x => x.ApplicationID == applicationId);
				if (skill != null)
				{
					skillId = skill.Id;
				}
			}
			return skillId;
		}

		public string GetSkillName(string applicationId)
		{
			int skillId = GetSkillId(applicationId);
			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				return db.Skills.Where(x => x.ApplicationID == applicationId).Select(x => x.Name).FirstOrDefault();
			}
		}

		public string GetWelcomeMessage(string applicationId)
		{
			int skillId = GetSkillId(applicationId);

			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				List<WelcomeMessage> messages = db.WelcomeMessages.Where(x => x.SkillId == skillId).ToList<WelcomeMessage>();
				if (messages != null && messages.Count > 0)
				{
					Random rnd = new Random();
					int r = rnd.Next(messages.Count);

					return messages[r].Message;
				}
			}
			return null;
		}

		public IntentMessageModel GetAnyIntentMessage(string applicationId, string userId)
		{
			int skillId = GetSkillId(applicationId);

			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				List<Intent> intents = db.Intents.Where(x => x.SkillId == skillId).ToList<Intent>();
				if (intents != null && intents.Count > 0)
				{
					Random rnd = new Random();
					int r = rnd.Next(intents.Count);

					return GetIntentMessage(applicationId, intents[r].Name, userId);
				}
			}
			return null;
		}

		public IntentMessageModel GetIntentMessage(string applicationId, string intentName, string userId)
		{
			int skillId = GetSkillId(applicationId);
			IntentMessage newMessage = null;
			using (var db = new AlexaSkillEntities())
			{
				Intent intent = db.Intents.FirstOrDefault(x => x.Name == intentName && x.SkillId == skillId);
				if (intent != null)
				{
					List<IntentMessage> messages = intent.IntentMessages.ToList();
					if(messages.Count == 0)
					{
						return null;
					}

					List<int> readedIds = db.IntentMessagesReads.Where(x => x.UserId == userId).Select(x => x.IntentMessageId).ToList();
					//Never Read
					List<IntentMessage> unreadMessages = messages.Where(x => !readedIds.Contains(x.Id)).ToList();
					if (unreadMessages.Count > 0)
					{
						newMessage = GetRandomMessage(unreadMessages);
						db.IntentMessagesReads.Add(new IntentMessagesRead() { 
							IntentMessageId = newMessage.Id,
							UserId = userId,
							ReadSecond = false
						});
						db.SaveChanges();
						return new IntentMessageModel()
						{
							Mesasge = newMessage.Message,
							ShouldEndSession = intent.ShouldEndSession
						};
					};

					//Not 2 times read
					unreadMessages = messages.Where(x => x.IntentMessagesReads.Any(y => y.UserId == userId && y.ReadSecond == false)).ToList();
					if (unreadMessages.Count > 0)
					{
						

						newMessage = GetRandomMessage(unreadMessages);
						IntentMessagesRead readedMessage = db.IntentMessagesReads.FirstOrDefault(x => x.IntentMessageId == newMessage.Id);
						if(readedMessage != null)
						{
							readedMessage.ReadSecond = true;
							db.SaveChanges();
						}
						return new IntentMessageModel()
						{
							Mesasge = newMessage.Message,
							ShouldEndSession = intent.ShouldEndSession
						};
					}

					//All read 2 times
					newMessage = GetRandomMessage(messages);
					List<int> messageIds = messages.Select(person => person.Id).ToList();
					

					List<IntentMessagesRead> readedMessages = db.IntentMessagesReads.Where(x => x.UserId == userId && messageIds.Contains(x.IntentMessageId)).ToList();
					foreach (IntentMessagesRead readedMessage in readedMessages)
					{
						if(newMessage.Id != readedMessage.IntentMessageId)
						{ 
							readedMessage.ReadSecond = false;
						}
					}
					db.SaveChanges();


					return new IntentMessageModel()
					{
						Mesasge = newMessage.Message,
						ShouldEndSession = intent.ShouldEndSession
					};
				}
			}
			return null;
		}

		private IntentMessage GetRandomMessage(List<IntentMessage> messages)
		{
			Random rnd = new Random();
			int r = rnd.Next(messages.Count);
			return messages[r];
		}
	}
}