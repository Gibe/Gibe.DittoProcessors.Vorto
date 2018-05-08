using Gibe.DittoProcessors.Vorto.Wrappers;
using Ninject.Modules;

namespace Gibe.DittoProcessors.Vorto.Ninject
{
	public abstract class BaseModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IHttpContextWrapper>().To<HttpContextWrapper>();
			Bind<IVortoWrapper>().To<VortoWrapper>();
		}
	}
}