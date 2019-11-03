using System;
using Framework.Selenium;
using OpenQA.Selenium;

namespace Royale.Pages
{
    public class HeaderNav
    {
        public readonly HeaderNavMap Map;

        public HeaderNav()
        {
            Map = new HeaderNavMap();
        }

        public void GotoCardsPage()
        {
            Map.CardsTabLink.Click();
        }
    }

    public class HeaderNavMap
    {
        public Element CardsTabLink => Driver.FindElement(By.CssSelector("a[href='/cards']"), "Cards Link");

        public Element DeckBuilderLink => Driver.FindElement(By.CssSelector("a[href='/deckbuilder']"), "Deck Builder Link");
    }
}
