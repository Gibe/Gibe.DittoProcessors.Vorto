using Gibe.DittoProcessors.Vorto.Services;

namespace Gibe.DittoProcessors.Vorto.Ninject
{
	public class UrlSuffixLanguageDittoProcessorsVortoModule : BaseModule
	{
		public override void Load()
		{
			base.Load();

			Bind<ILanguageDetectionService>().To<UrlLanguageDetectionService>();
		}
	}
}
