using Framework.Selenium;
using OpenQA.Selenium;

namespace Royale.Pages
{
    public class CopyDeckPage
    {
        public readonly CopyDeckPageMap Map;

        public CopyDeckPage()
        {
            Map = new CopyDeckPageMap();
        }

        public CopyDeckPage Yes()
        {
            Map.YesButton.Click();
            Driver.Wait.Until(drvr => Map.CopiedMessage.Displayed);
            return this;
        }

        public CopyDeckPage No()
        {
            Map.NoButton.Click();
            AcceptCookies();
            Driver.Wait.Until(drvr => Map.OtherStoresButton.Displayed);
            return this;
        }

        public void AcceptCookies()
        {
            Map.AcceptCookiesButton.Click();
            Driver.Wait.Until(drvr => !Map.AcceptCookiesButton.Displayed);
        }

        public void OpenAppStore()
        {
            Map.AppStoreButton.Click();
        }

        public void OpenGooglePlay()
        {
            Map.GooglePlayButton.Click();
        }
    }

    public class CopyDeckPageMap
    {
        public IWebElement YesButton => Driver.FindElement(By.Id("button-open"));

        public IWebElement CopiedMessage => Driver.FindElement(By.CssSelector(".notes.active"));

        public IWebElement NoButton => Driver.FindElement(By.Id("not-installed"));

        public IWebElement AppStoreButton => Driver.FindElement(By.XPath("//a[text()='App Store']"));

        public IWebElement GooglePlayButton => Driver.FindElement(By.XPath("//a[text()='Google Play']"));

        public IWebElement AcceptCookiesButton => Driver.FindElement(By.CssSelector("a.cc-btn.cc-dismiss"));

        public IWebElement OtherStoresButton => Driver.FindElement(By.Id("other-stores"));
    }
}
