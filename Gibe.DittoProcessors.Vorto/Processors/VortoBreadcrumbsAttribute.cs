using System;
using System.Collections.Generic;
using System.Linq;
using Gibe.DittoProcessors.Processors;
using Gibe.DittoServices.ModelConverters;
using Gibe.UmbracoWrappers;
using Our.Umbraco.Vorto.Extensions;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;


namespace Gibe.DittoProcessors.Vorto.Processors
{
	public class VortoBreadcrumbsAttribute : BaseVortoAttribute
	{
		public Func<IUmbracoWrapper> UmbracoWrapper { get; set; } = Inject<IUmbracoWrapper>();
		public Func<IModelConverter> ModelConverter { get; set; } = Inject<IModelConverter>();


		public bool _autoGenerateHomeCrumb { get; set; } = true;

		private readonly int _minDepth;
		private readonly List<string> _ignoredDocumentTypes;

		public VortoBreadcrumbsAttribute(string propertyAlias, string cultureName = null, string fallbackCultureName = null, int minDepth = 1, string ignoredDocumentTypes = null, bool autoGenerateHomeCrumb = true)
			: base(propertyAlias, cultureName: cultureName, fallbackCultureName: fallbackCultureName)
		{
			_minDepth = minDepth;
			_ignoredDocumentTypes = ignoredDocumentTypes?.ToDelimitedList(",")?.ToList() ?? new List<string>();
			_autoGenerateHomeCrumb = autoGenerateHomeCrumb;
		}

		public override object ProcessValue()
		{
			var content = Context.Content;

			IEnumerable<BreadcrumbItemModel> breadcrumbs = new List<BreadcrumbItemModel>();

			if (_autoGenerateHomeCrumb)
				breadcrumbs = breadcrumbs.Concat(HomePage());

			breadcrumbs = breadcrumbs.Concat(AncestorsPages(content).Reverse());

			if (ValidDocumentType(content))
				breadcrumbs = breadcrumbs.Concat(CurrentPage(content));

			return breadcrumbs;
		}

		private IEnumerable<BreadcrumbItemModel> CurrentPage(IPublishedContent content)
		{
			var menuName = VortoName(content);
			var breadcrumbs = new List<BreadcrumbItemModel>
			{
				new BreadcrumbItemModel(menuName, content.Url, true)
			};
			return breadcrumbs;
		}

		private IEnumerable<BreadcrumbItemModel> AncestorsPages(IPublishedContent content)
		{
			foreach (var item in UmbracoWrapper()
				.Ancestors(content)
				.Where(i => i.IsVisible() && !string.IsNullOrWhiteSpace(i.DocumentTypeAlias) && ValidDocumentType(i))
				)
			{
				if (item.Level <= _minDepth) break;
				yield return new BreadcrumbItemModel(VortoName(item), item.Url, false);
			}
		}

		private IEnumerable<BreadcrumbItemModel> HomePage()
		{
			yield return new BreadcrumbItemModel("Home", "/", false);
		}

		private bool ValidDocumentType(IPublishedContent content)
		{
			return !_ignoredDocumentTypes.Exists(dt => dt.InvariantEquals(content.DocumentTypeAlias));
		}

		private string VortoName(IPublishedContent content)
		{
			var name = string.Empty;

			if (content.HasProperty(_propertyAlias))
			{
				name = content.GetVortoValue<string>(_propertyAlias, cultureName: _cultureName, fallbackCultureName: _fallbackCultureName);
			}

			if (string.IsNullOrWhiteSpace(name))
				return content.Name;
			else
				return name;
		}
	}
}
