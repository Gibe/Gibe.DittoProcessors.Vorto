using System;
using Gibe.DittoProcessors.Processors;
using Gibe.DittoProcessors.Vorto.Services;
using Our.Umbraco.Vorto.Extensions;
using Umbraco.Core.Models;

namespace Gibe.DittoProcessors.Vorto.Processors
{
	public class VortoValue : InjectableProcessorAttribute
	{
		private readonly string _propertyAlias;
		private const string DefaultLanguageCode = "en-GB";
		private readonly Func<ILanguageDetectionService> LanguageDetectionService = Inject<ILanguageDetectionService>();

		public VortoValue(string propertyAlias)
		{
			_propertyAlias = propertyAlias;
		}

		public override object ProcessValue()
		{
			var value = (IPublishedContent)Value;

			var languageCode = LanguageDetectionService().LanguageCode();

			var vortoValue = value.GetVortoValue(_propertyAlias, languageCode);

			return value.GetVortoValue(_propertyAlias, vortoValue == null ? DefaultLanguageCode : languageCode);
		}

	}
}