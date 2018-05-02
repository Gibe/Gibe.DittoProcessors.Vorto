using System;
using System.Configuration;
using Gibe.DittoProcessors.Processors;
using Gibe.DittoProcessors.Vorto.Services;
using Our.Umbraco.Vorto.Extensions;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Gibe.DittoProcessors.Vorto.Processors
{
	public class VortoValueAttribute : InjectableProcessorAttribute
	{
		public string CultureName { get; set; }
		public bool Recursive { get; set; }
		public object DefaultValue { get; set; }
		public string FallbackCultureName { get; set; }

		private readonly string _propertyAlias;
		private readonly Func<ILanguageDetectionService> LanguageDetectionService = Inject<ILanguageDetectionService>();

		public VortoValueAttribute(string propertyAlias, string cultureName = null, bool recursive = false, object defaultValue = null, string fallbackCultureName = null)
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

			if (Recursive)
			{
				// NB - there is a bug in Vorto which means you get a null reference exception when a property doesn't exist on a content item
				// This is a workaround until a fix is provided
				content = RecursiveContentItem(content);
			}

			if (content == null || !content.HasValue(_propertyAlias))
				return null;


			return content.GetVortoValue(_propertyAlias, cultureName: CultureName, /* recursive: Recursive , */ defaultValue: DefaultValue, fallbackCultureName: FallbackCultureName);
		}

		private IPublishedContent RecursiveContentItem(IPublishedContent content)
		{
			if (content.HasValue(_propertyAlias))
				return content;

			return RecursiveContentItem(content.Parent);
		}
	}
}