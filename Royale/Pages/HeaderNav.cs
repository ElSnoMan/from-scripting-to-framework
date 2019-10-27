using System;
using OpenQA.Selenium;

namespace Royale.Pages
{
    public class HeaderNav
    {
        public readonly HeaderNavMap Map;

        public HeaderNav(IWebDriver driver)
        {
            Map = new HeaderNavMap(driver);
        }

        public void GotoCardsPage()
        {
            Map.CardsTabLink.Click();
        }
    }

    public class HeaderNavMap
    {
        IWebDriver _driver;

        public HeaderNavMap(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement CardsTabLink => _driver.FindElement(By.CssSelector("a[href='/cards']"));
    }
}
