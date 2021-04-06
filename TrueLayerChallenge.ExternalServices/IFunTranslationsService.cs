using System.Threading.Tasks;
using TrueLayerChallenge.Entities;

namespace TrueLayerChallenge.ExternalServices
{
    public interface IFunTranslationsService
    {
        Task<string> TranslateAsync(string message, TranslationType translationType);
    }
}