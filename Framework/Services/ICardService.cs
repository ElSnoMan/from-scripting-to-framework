using Framework.Models;

namespace Framework.Services
{
    public interface ICardService
    {
        Card GetCardByName(string cardName);
    }
}
