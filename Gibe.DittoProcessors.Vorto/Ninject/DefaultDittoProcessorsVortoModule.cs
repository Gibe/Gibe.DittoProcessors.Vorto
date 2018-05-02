using Gibe.DittoProcessors.Vorto.Services;
using Gibe.DittoProcessors.Vorto.Wrappers;
using Ninject.Modules;

namespace Gibe.DittoProcessors.Vorto.Ninject
{
	public class DefaultDittoProcessorsVortoModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IHttpContextWrapper>().To<HttpContextWrapper>();
			Bind<ILanguageDetectionService>().To<VortoLanguageDetectionService>();
		}
	}
}
