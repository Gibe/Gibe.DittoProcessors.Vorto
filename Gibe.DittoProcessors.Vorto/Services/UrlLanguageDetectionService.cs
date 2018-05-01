using Gibe.DittoProcessors.Vorto.Wrappers;
using System.Linq;
using System.Text.RegularExpressions;

namespace Gibe.DittoProcessors.Vorto.Services
{
	public class UrlLanguageDetectionService : ILanguageDetectionService
	{
		private const string DefaultLanguageCode = "en-GB";
		private const string ExpectedShortLanguageCodePattern = "^[a-z]{2}$";
		private const string ExpectedLongLanguageCodePattern = "^[a-z]{2}-[A-Z]{2}$";
		private const string LowerCaseLongLanguageCodePattern = "^[a-z]{2}-[a-z]{2}$";

		private readonly IHttpContextWrapper _httpContextWrapper;

		public UrlLanguageDetectionService(IHttpContextWrapper httpContextWrapper)
		{
			_httpContextWrapper = httpContextWrapper;
		}

		public string LanguageCode()
		{
			var currentUrl = _httpContextWrapper.AbsolutePath();
			var languageCode = currentUrl.Split('/').Last();

			if (IsValidLanguageCode(languageCode, ExpectedShortLanguageCodePattern) || IsValidLanguageCode(languageCode, ExpectedLongLanguageCodePattern))
			{
				return languageCode;
			}

			if (IsValidLanguageCode(languageCode, LowerCaseLongLanguageCodePattern))
			{
				return LongLanguageCodeWithUpperCaseSuffix(languageCode);
			}

			return DefaultLanguageCode;
		}

		private bool IsValidLanguageCode(string urlLanguageCode, string expectedPattern)
		{
			var languageCodeRegex = new Regex(expectedPattern);
			return languageCodeRegex.Match(urlLanguageCode).Success;
		}


		private string LongLanguageCodeWithUpperCaseSuffix(string validUrlLanguageCode)
		{
			var splitLanguageCode = validUrlLanguageCode.Split('-');
			return $"{splitLanguageCode[0]}-{splitLanguageCode[1].ToUpper()}";
		}
	}
}