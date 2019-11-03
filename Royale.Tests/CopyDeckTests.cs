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

        [Test, Category("copydeck")]
        public void User_can_copy_the_deck()
        {
            Pages.DeckBuilder.Goto().AddCardsManually();
            Pages.DeckBuilder.CopySuggestedDeck();
            Pages.CopyDeck.Yes();
            Assert.That(Pages.CopyDeck.Map.CopiedMessage.Displayed);
        }

        [Test, Category("copydeck")]
        public void User_opens_app_store()
        {
            Pages.DeckBuilder.Goto().AddCardsManually();
            Pages.DeckBuilder.CopySuggestedDeck();
            Pages.CopyDeck.No().OpenAppStore();
            Assert.That(Driver.Title, Is.EqualTo("Clash Royale on the App Store"));
        }

        [Test, Category("copydeck")]
        public void User_opens_google_play()
        {
            Pages.DeckBuilder.Goto().AddCardsManually();
            Pages.DeckBuilder.CopySuggestedDeck();
            Pages.CopyDeck.No().OpenGooglePlay();
            Assert.AreEqual("Clash Royale - Apps on Google Play", Driver.Title);
        }
    }
}
