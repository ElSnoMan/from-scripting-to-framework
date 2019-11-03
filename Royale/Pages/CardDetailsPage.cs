using System;
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

        public (string Type, int Arena) GetCardCategory()
        {
            var categories = Map.CardCategory.Text.Split(','); // "Troop, Arena 8" => ["Troop", " Arena 8"]
            var type = categories[0].ToLower(); // "Troop" => "troop"
            var arena = categories[1].Trim().Split(' ').Last(); // " Arena 8" => "8"

            int intArena;
            if (int.TryParse(arena, out intArena))
            {
                return (type, intArena);
            }
            else
            {
                // The Arena was "Training Camp" and should return 0
                return (type, 0);
            }
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
        public Element CardName => Driver.FindElement(By.CssSelector("div[class*='cardName']"), "Card Name");

        public Element CardCategory => Driver.FindElement(By.CssSelector("div[class*='card__rarity']"), "Card Category");

        public Element CardRarity => Driver.FindElement(By.CssSelector("div[class*='rarityCaption']"), "Card Rarity");
    }
}
