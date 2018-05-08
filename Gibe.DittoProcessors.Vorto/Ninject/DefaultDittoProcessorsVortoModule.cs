using Gibe.DittoProcessors.Vorto.Services;

namespace Gibe.DittoProcessors.Vorto.Ninject
{
	public class DefaultDittoProcessorsVortoModule : BaseModule
	{
		public override void Load()
		{
			base.Load();

			Bind<ILanguageDetectionService>().To<VortoLanguageDetectionService>();
		}
	}
}
