using Our.Umbraco.Vorto.Extensions;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Gibe.DittoProcessors.Vorto.Processors
{
	public class VortoValueAttribute : BaseVortoAttribute
	{
		public VortoValueAttribute(string propertyAlias, string cultureName = null, bool recursive = false, object defaultValue = null, string fallbackCultureName = null)
			: base(propertyAlias, cultureName, recursive, defaultValue, fallbackCultureName)
		{
		}

		public override object ProcessValue()
		{
			var content = (IPublishedContent)Value;

			if (_recursive)
			{
				// NB - there is a bug in Vorto which means you get a null reference exception when a property doesn't exist on a content item
				// This is a workaround until a fix is provided
				content = RecursiveContentItem(content);
			}

			if (content == null || !content.HasValue(_propertyAlias))
				return null;


			return content.GetVortoValue(_propertyAlias, cultureName: _cultureName, /* recursive: Recursive , */ defaultValue: _defaultValue, fallbackCultureName: _fallbackCultureName);
		}

		private IPublishedContent RecursiveContentItem(IPublishedContent content)
		{
			if (content.HasValue(_propertyAlias))
				return content;

			return RecursiveContentItem(content.Parent);
		}
	}
}