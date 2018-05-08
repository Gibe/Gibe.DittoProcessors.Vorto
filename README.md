# DittoProcessors.Vorto

## Installing

Install via nuget ``` install-package Gibe.DittoProcessors.Vorto ```

2 Ninject Modules are available:

* ``` Gibe.DittoProcessors.Vorto.Ninject.DefaultDittoProcessorsVortoModule ``` - determines the current language from the current UI culture
* ``` Gibe.DittoProcessors.Vorto.Ninject.UrlSuffixLanguageDittoProcessorsVortoModule ``` - determines the current language from a suffix on the URL

## Processors
| Processor | Description |
|:----------|:------------|
|VortoValue| Return the value of ```GetVortoValue``` of an ```IPublishedContent``` |
|VortoBreadcrumbsValue| Get a List<> of breadcrumbs relating to the current page |

## Wrappers
There is an `IVortoWrapper` class which can be used to wrap calls to Vorto

# DittoServices

Install via nuget ``` install-package Gibe.DittoServices ```

Ninject Module is available ``` Gibe.DittoServices.Ninject.DittoServicesModule ```
