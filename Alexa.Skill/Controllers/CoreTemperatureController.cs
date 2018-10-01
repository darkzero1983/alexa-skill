using Alexa.Skill.Context;
using Alexa.Skill.Models;
using Alexa.Skill.Models.CoreTemperature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alexa.Skill.Controllers
{
	public class CoreTemperatureController : Controller
	{
		#region Overview
		public ActionResult Overview(int skillId)
		{
			CoreTemperatureOverviewViewModel model = new CoreTemperatureOverviewViewModel()
			{
				SkillId = skillId
			};

			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				List<CoreTemperature> coreTemperatures = db.CoreTemperatures.OrderBy(x => x.Name) .ToList();

				foreach (CoreTemperature coreTemperature in coreTemperatures)
				{
					model.CoreTemperatures.Add(new CoreTemperatureOverviewItemViewModel()
					{
						Id = coreTemperature.Id,
						Name = coreTemperature.Name
					});
				}
			}
			return View(model);
		}
		#endregion

		#region Edit Temperature
		[HttpGet]
		public ActionResult EditTemperature(int skillId, int? temperatureId)
		{
			TemperatureEditViewModel model = new TemperatureEditViewModel()
			{
				SkillId = skillId,
				TemperatureId = temperatureId
			};

			if( temperatureId != null && temperatureId != 0)
			{
				using (AlexaSkillEntities db = new AlexaSkillEntities())
				{
					CoreTemperature temperature = db.CoreTemperatures.FirstOrDefault(x => x.Id == temperatureId);
					model.Name = temperature.Name;
					model.DefaultDegree = temperature.DefaultDegree;
					model.DefaultMaxDegree = temperature.DefaultMaxDegree;
					model.DefaultMinDegree = temperature.DefaultMinDegree;
					model.DoneDegree = temperature.DoneDegree;
					model.DoneMaxDegree = temperature.DoneMaxDegree;
					model.DoneMinDegree = temperature.DoneMinDegree;
					model.MediumDegree = temperature.MediumDegree;
					model.MediumMaxDegree = temperature.MediumMaxDegree;
					model.MediumMinDegree = temperature.MediumMinDegree;
					model.RareDegree = temperature.RareDegree;
					model.RareMaxDegree = temperature.RareMaxDegree;
					model.RareMinDegree = temperature.RareMinDegree;

				}
			}
			

			return View(model);
		}

		[HttpPost]
		public ActionResult EditTemperature(TemperatureEditViewModel model)
		{
			CoreTemperature temperature = new CoreTemperature();

			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				if (model.TemperatureId != null && model.TemperatureId != 0)
				{
					temperature = db.CoreTemperatures.FirstOrDefault(x => x.Id == model.TemperatureId);
				}
				
				if(db.CoreTemperatures.Where(x => x.Name == model.Name && x.Id != model.TemperatureId).Count() > 0)
				{
					return RedirectToAction("Overview", "CoreTemperature", new { skillId = model.SkillId });
				}

				if (temperature != null)
				{ 
					temperature.Name = model.Name;
					temperature.DefaultDegree = model.DefaultDegree;
					temperature.DefaultMaxDegree = model.DefaultMaxDegree;
					temperature.DefaultMinDegree = model.DefaultMinDegree;
					temperature.DoneDegree = model.DoneDegree;
					temperature.DoneMaxDegree = model.DoneMaxDegree;
					temperature.DoneMinDegree = model.DoneMinDegree;
					temperature.MediumDegree = model.MediumDegree;
					temperature.MediumMaxDegree = model.MediumMaxDegree;
					temperature.MediumMinDegree = model.MediumMinDegree;
					temperature.RareDegree = model.RareDegree;
					temperature.RareMaxDegree = model.RareMaxDegree;
					temperature.RareMinDegree = model.RareMinDegree;

					if (model.TemperatureId == null || model.TemperatureId == 0)
					{
						db.CoreTemperatures.Add(temperature);
					}
				}
				db.SaveChanges();
			}
			return RedirectToAction("Overview", "CoreTemperature", new { skillId = model.SkillId});
		}
		#endregion

		#region Edit TemperatureCalls
		[HttpGet]
		public ActionResult EditTemperatureCalls(int skillId, int temperatureId)
		{
			TemperatureCallViewModel model = new TemperatureCallViewModel()
			{
				SkillId = skillId,
				TemperatureId = temperatureId
			};

			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				if(temperatureId != 0)
				{
					CoreTemperature temperature = db.CoreTemperatures.FirstOrDefault(x => x.Id == temperatureId);
					if(temperature != null)
					{
						model.TemperatureName = temperature.Name;
					}
				}
				List<Meat> meats = db.Meats.Where(x => x.CoreTemperatureId == temperatureId).ToList();

				foreach (Meat meat in meats)
				{
					model.Calls.Add(new TemperatureCallItemViewModel()
					{
						Id = meat.Id,
						Name = meat.Name
					});
				}
				db.SaveChanges();
			}

			return View(model);
		}

		[HttpPost]
		public ActionResult EditTemperatureCalls(TemperatureCallViewModel model)
		{
			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				if(db.Meats.Where(x => x.Name == model.AddCallName).Count() > 0)
				{
					return RedirectToAction("EditTemperatureCalls", "CoreTemperature", new { skillId = model.SkillId, temperatureId = model.TemperatureId });
				}

				Meat meat = new Meat()
				{
					Name = model.AddCallName,
					CoreTemperatureId = model.TemperatureId
				};
				db.Meats.Add(meat);
				db.SaveChanges();
			}

			return RedirectToAction("EditTemperatureCalls", "CoreTemperature", new { skillId = model.SkillId, temperatureId = model.TemperatureId});
		}
		#endregion

		#region Edit Answers
		public ActionResult AnswerOverview(int skillId)
		{
			AnswerOverviewViewModel model = new AnswerOverviewViewModel()
			{
				SkillId = skillId
			};

			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				List<Answer> answers = db.Answers.OrderBy(x => x.Answert).ToList();

				foreach (Answer answer in answers)
				{
					model.Answers.Add(new AnswerOverviewItemViewModel()
					{
						Id = answer.Id,
						Answer = answer.Answert
					});
				}
			}
			return View(model);
		}
		#endregion
		
		#region Edit Answer
		[HttpGet]
		public ActionResult EditAnswer(int skillId, int? answerId)
		{
			AnswerEditViewModel model = new AnswerEditViewModel()
			{
				SkillId = skillId,
				AnswereId = answerId
			};

			if (answerId != null && answerId != 0)
			{
				using (AlexaSkillEntities db = new AlexaSkillEntities())
				{
					Answer answer = db.Answers.FirstOrDefault(x => x.Id == answerId);
					model.Answer = answer.Answert;
					model.ShouldEndSession = answer.ShouldEndSession;
					model.SessionTerm = answer.SessionTerm;

				}
			}
			
			return View(model);
		}

		[HttpPost]
		public ActionResult EditAnswer(AnswerEditViewModel model)
		{
			Answer answer = new Answer();

			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				if (model.AnswereId != null && model.AnswereId != 0)
				{
					answer = db.Answers.FirstOrDefault(x => x.Id == model.AnswereId);
				}

				if (db.Answers.Where(x => x.Answert == model.Answer && x.Id != model.AnswereId).Count() > 0)
				{
					return RedirectToAction("Overview", "CoreTemperature", new { skillId = model.SkillId });
				}

				if (answer != null)
				{
					answer.Answert = model.Answer;
					answer.SessionTerm = model.SessionTerm;
					answer.ShouldEndSession = model.ShouldEndSession;

					if (model.AnswereId == null || model.AnswereId == 0)
					{
						db.Answers.Add(answer);
					}
				}
				db.SaveChanges();
			}
			return RedirectToAction("AnswerOverview", "CoreTemperature", new { skillId = model.SkillId });
		}
		#endregion
		
		#region Edit AnswerCalls
		[HttpGet]
		public ActionResult EditAnswerCalls(int skillId, int answerId)
		{
			AnswerCallViewModel model = new AnswerCallViewModel()
			{
				SkillId = skillId,
				AnswerId = answerId
			};

			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				if (answerId != 0)
				{
					Answer answer = db.Answers.FirstOrDefault(x => x.Id == answerId);
					if (answer != null)
					{
						model.Answer = answer.Answert;
					}
				}
				List<Meat> meats = db.Meats.Where(x => x.AnswerId == answerId).ToList();

				foreach (Meat meat in meats)
				{
					model.Calls.Add(new AnswerCallItemViewModel()
					{
						Id = meat.Id,
						Name = meat.Name
					});
				}
				db.SaveChanges();
			}

			return View(model);
		}

		[HttpPost]
		public ActionResult EditAnswerCalls(AnswerCallViewModel model)
		{
			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				if (db.Meats.Where(x => x.Name == model.AddCallName).Count() > 0)
				{
					return RedirectToAction("EditAnswerCalls", "CoreTemperature", new { skillId = model.SkillId, answerId = model.AnswerId });
				}

				Meat meat = new Meat()
				{
					Name = model.AddCallName,
					AnswerId = model.AnswerId
				};
				db.Meats.Add(meat);
				db.SaveChanges();
			}

			return RedirectToAction("EditAnswerCalls", "CoreTemperature", new { skillId = model.SkillId, answerId = model.AnswerId });
		}
		#endregion

		#region Edit User Calls
		public ActionResult UserCallOverview(int skillId, int? answerId, int? coreTemperatureId)
		{
			UserCallOverviewViewModel model = new UserCallOverviewViewModel()
			{
				SkillId = skillId,
				Answers = GetAnswers(),
				CoreTemperatures = GetCoreTemperature()
		};

			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				List<Meat> userCalls;
				
				if(answerId == null && coreTemperatureId == null)
				{
					userCalls = db.Meats.OrderBy(x => x.Name).ToList();

				}
				else if(answerId.HasValue)
				{
					userCalls = db.Meats.Where(x => x.AnswerId == answerId).OrderBy(x => x.Name).ToList();
				}
				else if (coreTemperatureId.HasValue)
				{
					userCalls = db.Meats.Where(x => x.CoreTemperatureId == coreTemperatureId).OrderBy(x => x.Name).ToList();
				}
				else
				{
					userCalls = db.Meats.OrderBy(x => x.Name).ToList();
				}

				foreach (Meat userCall in userCalls)
				{
					UserCallViewModel newUserCall = new UserCallViewModel()
					{
						UserCallId = userCall.Id,
						UserCallName = userCall.Name
					};

					if(userCall.AnswerId != null)
					{
						newUserCall.AnswerId = userCall.AnswerId;
						newUserCall.AnswerText = userCall.Answer.Answert;
					}

					if (userCall.CoreTemperatureId != null)
					{
						newUserCall.CoreTemperatureId = userCall.CoreTemperatureId;
						newUserCall.CoreTemeratureName = userCall.CoreTemperature.Name;
					}

					model.UserCalls.Add(newUserCall);
				}
			}
			return View(model);
		}


		private List<SelectListItem> GetAnswers()
		{
			List<SelectListItem> answers = new List<SelectListItem>()
			{
				new SelectListItem(){ Text = "--- Keine Auswahl ---", Value = ""}
			};
			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				foreach (Answer answer in db.Answers.OrderBy(x => x.Answert).ToList())
				{
					answers.Add(new SelectListItem()
					{
						Value = answer.Id.ToString(),
						Text = answer.Answert
					});
				}
			}

			return answers;
		}

		private List<SelectListItem> GetCoreTemperature()
		{
			List<SelectListItem> coreTemperatures = new List<SelectListItem>()
			{
				new SelectListItem(){ Text = "--- Keine Auswahl ---", Value = ""}
			};
			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				foreach (CoreTemperature coreTemperature in db.CoreTemperatures.OrderBy(x => x.Name).ToList())
				{
					coreTemperatures.Add(new SelectListItem()
					{
						Value = coreTemperature.Id.ToString(),
						Text = coreTemperature.Name
					});
				}
			}

			return coreTemperatures;
		}

		[HttpGet]
		public ActionResult UserCallEdit(int skillId, int? userCallId)
		{
			UserCallEditViewModel model = new UserCallEditViewModel()
			{
				SkillId = skillId
			};

			
			

			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				if(userCallId != null && userCallId != 0)
				{
					Meat userCall = db.Meats.FirstOrDefault(x => x.Id == userCallId);
					if(userCall != null)
					{
						model.UserCallId = userCall.Id;
						model.UserCallName = userCall.Name;
						model.AnswerId = userCall.AnswerId;
						model.CoreTemperatureId = userCall.CoreTemperatureId;
					}
				}
				

				
			}

			model.Answers = GetAnswers();
			model.CoreTemperatures = GetCoreTemperature();
			return View(model);
		}

		[HttpPost]
		public ActionResult UserCallEdit(UserCallEditViewModel model)
		{
			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				Meat meat = new Meat();
				if(model.UserCallId != 0)
				{
					meat = db.Meats.FirstOrDefault(x => x.Id == model.UserCallId);
				}
				if(meat == null)
				{
					meat = new Meat();
				}
				meat.Name = model.UserCallName;
				meat.AnswerId = model.AnswerId;
				meat.CoreTemperatureId = model.CoreTemperatureId;
				if(meat.Id == 0)
				{
					db.Meats.Add(meat);
				}
				db.SaveChanges();
			}

			return RedirectToAction("UserCallOverview", new { skillId = model.SkillId });
		}

		[HttpGet]
		public ActionResult UserCallDelete(int skillId, int userCallId)
		{
			using (AlexaSkillEntities db = new AlexaSkillEntities())
			{
				Meat meat = db.Meats.FirstOrDefault(x => x.Id == userCallId);
				if(meat != null)
				{
					db.Meats.Remove(meat);
					db.SaveChanges();
				}
			}
			
			return RedirectToAction("UserCallOverview", new { skillId = skillId });
		}
		#endregion
	}
}