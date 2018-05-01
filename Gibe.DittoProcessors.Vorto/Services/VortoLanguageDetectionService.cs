using System.Threading;

namespace Gibe.DittoProcessors.Vorto.Services
{
	public class VortoLanguageDetectionService : ILanguageDetectionService
	{
		public string LanguageCode() => null; // Returns a null value so that Vorto will use its in-built language detection
	}
}
