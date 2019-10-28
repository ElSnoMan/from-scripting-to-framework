using System.Linq;
using Framework.Models;
using Framework.Selenium;
using OpenQA.Selenium;

namespace Royale.Pages
{
    public class CardDetailsPage : PageBase
    {
        public readonly CardDetailsPageMap Map;

        public CardDetailsPage()
        {
            Map = new CardDetailsPageMap();
        }

        public (string Type, string Arena) GetCardCategory()
        {
            var categories = Map.CardCategory.Text.Split(',');
            return (categories[0].ToLower(), categories[1].Trim());
        }

        public Card GetBaseCard()
        {
            var (type, arena) = GetCardCategory();

            return new Card
            {
                Name = Map.CardName.Text,
                Rarity = Map.CardRarity.Text.Split('\n').Last(),
                Type = type,
                Arena = arena
            };
        }
    }

    public class CardDetailsPageMap
    {
        public IWebElement CardName => Driver.FindElement(By.CssSelector("div[class*='cardName']"));

        public IWebElement CardCategory => Driver.FindElement(By.CssSelector("div[class*='card__rarity']"));

        public IWebElement CardRarity => Driver.FindElement(By.CssSelector("div[class*='rarityCaption']"));
    }
}
