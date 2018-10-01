using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alexa.Skill.Models.Skills
{
	public class SkillAddViewModel
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		[Display(Name = "Skill ID")]
		public string ApplicationID { get; set; }

		[Required]
		public int SkillTypeId { get; set; }

		[Display(Name = "Skill Arten")]
		public IEnumerable<SelectListItem> SkillTypes { get; set; }

		public SkillAddViewModel()
		{
			SkillTypes = new List<SelectListItem>();

		}
	}
}