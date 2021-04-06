using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TrueLayerChallenge.Entities;
using TrueLayerChallenge.Entities.FunTranslations;

namespace TrueLayerChallenge.ExternalServices
{
    public class FunTranslationsServiceSimulated : IFunTranslationsService
    {
        public FunTranslationsServiceSimulated() { }

        public async Task<string> TranslateAsync(string message, TranslationType translationType)
        {
            string path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"Samples\some-shakespeare.json");
            Translation translation = JsonSerializer.Deserialize<Translation>(System.IO.File.ReadAllText(path), new JsonSerializerOptions { PropertyNamingPolicy = new JsonSnakeCaseKeyNamingPolicy() });

            var value = new ValueTask<string>(translation?.Contents.Translated);

            return await value;
        }

    }
}
