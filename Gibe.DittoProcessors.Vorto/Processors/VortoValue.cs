using System;
using System.Configuration;
using Gibe.DittoProcessors.Processors;
using Gibe.DittoProcessors.Vorto.Services;
using Our.Umbraco.Vorto.Extensions;
using Umbraco.Core.Models;

namespace Gibe.DittoProcessors.Vorto.Processors
{
	public class VortoValue : InjectableProcessorAttribute
	{
		public string CultureName { get; set; }
		public bool Recursive { get; set; }
		public object DefaultValue { get; set; }
		public string FallbackCultureName { get; set; }

		private readonly string _propertyAlias;
		private readonly Func<ILanguageDetectionService> LanguageDetectionService = Inject<ILanguageDetectionService>();

		public VortoValue(string propertyAlias, string cultureName = null, bool recursive = false, object defaultValue = null, string fallbackCultureName = null)
		{
			_propertyAlias = propertyAlias;
			CultureName = cultureName ?? LanguageDetectionService().LanguageCode();
			Recursive = recursive;
			DefaultValue = defaultValue;
			FallbackCultureName = fallbackCultureName ?? ConfigurationManager.AppSettings["Gibe.DittoProcessors.Vorto:FallbackCultureName"] ?? "en-GB"; ;
		}

		public override object ProcessValue()
		{
			var content = (IPublishedContent)Value;

			return content.GetVortoValue(_propertyAlias, CultureName, Recursive, DefaultValue, FallbackCultureName);
		}
	}
}