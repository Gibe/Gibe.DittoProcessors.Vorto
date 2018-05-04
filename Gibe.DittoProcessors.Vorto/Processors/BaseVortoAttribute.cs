using Gibe.DittoProcessors.Processors;
using Gibe.DittoProcessors.Vorto.Services;
using System;
using System.Configuration;

namespace Gibe.DittoProcessors.Vorto.Processors
{
	public abstract class BaseVortoAttribute : InjectableProcessorAttribute
	{
		protected readonly Func<ILanguageDetectionService> LanguageDetectionService = Inject<ILanguageDetectionService>();

		protected readonly string _propertyAlias;
		protected string _cultureName;
		protected bool _recursive;
		protected object _defaultValue;
		protected string _fallbackCultureName;

		public BaseVortoAttribute(string propertyAlias, string cultureName = null, bool recursive = false, object defaultValue = null, string fallbackCultureName = null)
		{
			_propertyAlias = propertyAlias;
			_cultureName = cultureName ?? LanguageDetectionService().LanguageCode();
			_recursive = recursive;
			_defaultValue = defaultValue;
			_fallbackCultureName = fallbackCultureName ?? ConfigurationManager.AppSettings[Constants.FallbackCultureConfigKey] ?? Constants.DefaultCulture;
		}
	}
}
