using System.IO;
using System.Linq;
using Framework.Models;
using Framework.Services;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Royale.Pages;

namespace Tests
{
    public class CardTests
    {
        IWebDriver driver;

        [SetUp]
        public void BeforeEach()
        {
            driver = new ChromeDriver(Path.GetFullPath(@"../../../../" + "_drivers"));
            driver.Url = "https://statsroyale.com";
        }

        [TearDown]
        public void AfterEach()
        {
            driver.Quit();
        }

        [Test]
        public void Ice_Spirit_is_on_Cards_Page()
        {
            var cardsPage = new CardsPage(driver);
            var iceSpirit = cardsPage.Goto().GetCardByName("Ice Spirit");
            Assert.That(iceSpirit.Displayed);
        }

        [Test]
        public void Ice_Spirit_headers_are_correct_on_Card_Details_Page()
        {
            new CardsPage(driver).Goto().GetCardByName("Ice Spirit").Click();
            var cardDetails = new CardDetailsPage(driver);

            var (category, arena) = cardDetails.GetCardCategory();
            var cardName = cardDetails.Map.CardName.Text;
            var cardRarity = cardDetails.Map.CardRarity.Text.Split('\n').Last();

            Assert.AreEqual("Ice Spirit", cardName);
            Assert.AreEqual("Troop", category);
            Assert.AreEqual("Arena 8", arena);
            Assert.AreEqual("Common", cardRarity);
        }

        static string[] cardNames = { "Ice Spirit", "Mirror" };

        [Test, Category("cards")]
        [TestCaseSource("cardNames")]
        [Parallelizable(ParallelScope.Children)]
        public void Card_headers_are_correct_on_Card_Details_Page(string cardName)
        {
            new CardsPage(driver).Goto().GetCardByName(cardName).Click();
            var cardDetails = new CardDetailsPage(driver);

            var cardOnPage = cardDetails.GetBaseCard();
            var card = new InMemoryCardService().GetCardByName(cardName);

            Assert.AreEqual(card.Name, cardOnPage.Name);
            Assert.AreEqual(card.Type, cardOnPage.Type);
            Assert.AreEqual(card.Arena, cardOnPage.Arena);
            Assert.AreEqual(card.Rarity, cardOnPage.Rarity);
        }
    }
}
