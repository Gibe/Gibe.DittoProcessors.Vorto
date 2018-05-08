using Our.Umbraco.Vorto.Extensions;
using Umbraco.Core.Models;

namespace Gibe.DittoProcessors.Vorto.Wrappers
{
	public class VortoWrapper : IVortoWrapper
	{
		public T GetVortoValue<T>(IPublishedContent content, string propertyAlias, string cultureName = null, bool recursive = false, T defaultValue = default(T), string fallbackCultureName = null)
		{
			return content.GetVortoValue<T>(propertyAlias, cultureName, recursive, defaultValue, fallbackCultureName);
		}

		public object GetVortoValue(IPublishedContent content, string propertyAlias, string cultureName = null, bool recursive = false, object defaultValue = null, string fallbackCultureName = null)
		{
			return content.GetVortoValue(propertyAlias, cultureName, recursive, defaultValue, fallbackCultureName);
		}

		public bool HasVortoValue(IPublishedContent content, string propertyAlias, string cultureName = null, bool recursive = false, string fallbackCultureName = null)
		{
			return content.HasVortoValue(propertyAlias, cultureName, recursive, fallbackCultureName);
		}

		public bool IsVortoProperty(IPublishedContent content, string propertyAlias)
		{
			return content.IsVortoProperty(propertyAlias);
		}
	}
}