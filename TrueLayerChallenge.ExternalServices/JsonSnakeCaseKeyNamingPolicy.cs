using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TrueLayerChallenge.ExternalServices
{
    public class JsonSnakeCaseKeyNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string propertyName)
        {
            string jsonKey = Regex
                .Split(propertyName, @"(?<!^)(?=[A-Z])")
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.ToLower())
                .Aggregate("", (sum, item) => sum + (sum == "" ? "": "_") + item);

            return jsonKey;
        }
    }
}
