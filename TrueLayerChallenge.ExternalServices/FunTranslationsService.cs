using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueLayerChallenge.Entities;

namespace TrueLayerChallenge.ExternalServices
{
    public class FunTranslationsService : IFunTranslationsService
    {
        public const string URI = "https://api.funtranslations.com/translate/";

        private readonly ILogger<FunTranslationsService> _logger;
        private readonly IHttpClientService _client;

        public FunTranslationsService(ILogger<FunTranslationsService> logger, IHttpClientService client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<string> TranslateAsync(string message, TranslationType translationType)
        {
            string url = URI;
            switch (translationType)
            {
                case TranslationType.Shakespeare:
                    url = $"{URI}shakespeare.json";
                    break;
                case TranslationType.Yoda:
                    url = $"{URI}yoda.json";
                    break;
                default:
                    _logger.LogError($"Translation is not supported for: {translationType}");
                    return message;
            }

            message = "You gave Mr. Tim a hearty meal, but unfortunately what he ate made him die.";

            try
            {
                var response = await _client.GetAsync<Entities.FunTranslations.Translation>(url, new Dictionary<string, string> { { "text", message } });
                return response.Success.Total == 1 ? response.Contents.Translated : message;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Translation failed: {translationType} / {message}", ex);
                return message;
            }
        }

    }
}
