using Umbraco.Core.Models;

namespace Gibe.DittoProcessors.Vorto.Wrappers
{
	public interface IVortoWrapper
	{
		T GetVortoValue<T>(IPublishedContent content, string propertyAlias, string cultureName = null, bool recursive = false, T defaultValue = default(T), string fallbackCultureName = null);
		object GetVortoValue(IPublishedContent content, string propertyAlias, string cultureName = null, bool recursive = false, object defaultValue = null, string fallbackCultureName = null);
		bool HasVortoValue(IPublishedContent content, string propertyAlias, string cultureName = null, bool recursive = false, string fallbackCultureName = null);
		bool IsVortoProperty(IPublishedContent content, string propertyAlias);

	}
}
