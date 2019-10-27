using System.Linq;
using Framework.Models;
using OpenQA.Selenium;

namespace Royale.Pages
{
    public class CardDetailsPage : PageBase
    {
        public readonly CardDetailsPageMap Map;

        public CardDetailsPage(IWebDriver driver) : base(driver)
        {
            Map = new CardDetailsPageMap(driver);
        }

        public (string Category, string Arena) GetCardCategory()
        {
            var categories = Map.CardCategory.Text.Split(',');
            return (categories[0].Trim(), categories[1].Trim());
        }

        public Card GetBaseCard()
        {
            var (category, arena) = GetCardCategory();

            return new Card
            {
                Name = Map.CardName.Text,
                Rarity = Map.CardRarity.Text.Split('\n').Last(),
                Type = category,
                Arena = arena
            };
        }
    }

    public class CardDetailsPageMap
    {
        IWebDriver _driver;

        public CardDetailsPageMap(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement CardName => _driver.FindElement(By.CssSelector("div[class*='cardName']"));

        public IWebElement CardCategory => _driver.FindElement(By.CssSelector("div[class*='card__rarity']"));

        public IWebElement CardRarity => _driver.FindElement(By.CssSelector("div[class*='rarityCaption']"));
    }
}
