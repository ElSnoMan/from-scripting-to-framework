using System;
using Framework.Selenium;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Royale.Pages;

namespace Tests
{
    public class CopyDeckTests
    {
        [SetUp]
        public void BeforeEach()
        {
            Driver.Init();
            Pages.Init();
            Driver.Goto("https://statsroyale.com");
        }

        [TearDown]
        public void AfterEach()
        {
            Driver.Quit();
        }

        [Test]
        public void User_can_copy_the_deck()
        {
            Pages.DeckBuilder.Goto();
            Driver.Wait.Until(drvr => Pages.DeckBuilder.Map.AddCardsManuallyLink.Displayed);
            Pages.DeckBuilder.AddCardsManually();
            Driver.Wait.Until(drvr => Pages.DeckBuilder.Map.CopyDeckIcon.Displayed);

            Pages.DeckBuilder.CopySuggestedDeck();

            Pages.CopyDeck.Yes();
            Driver.Wait.Until(drvr => Pages.CopyDeck.Map.CopiedMessage.Displayed);

            Assert.That(Pages.CopyDeck.Map.CopiedMessage.Displayed);
        }
    }
}
