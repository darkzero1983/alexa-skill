using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alexa.Skill.Models.Statistic
{
	public class StatisitcViewModel
	{
		public int SkillId { get; set; }

		public List<RequestLogViewModel> RequestLogs { get; set; }

		public List<ChartPoint> ChartRequests { get; set; }
		public List<ChartPoint> ChartUsers { get; set; }

		public StatisticSettings Settings { get; set; }

		public StatisitcViewModel()
		{
			RequestLogs = new List<RequestLogViewModel>();
			ChartRequests = new List<ChartPoint>();
			ChartUsers = new List<ChartPoint>();
			Settings = new StatisticSettings();
		}
	}
}