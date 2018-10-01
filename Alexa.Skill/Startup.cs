using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Alexa.Skill.Startup))]
namespace Alexa.Skill
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);
		}
	}
}