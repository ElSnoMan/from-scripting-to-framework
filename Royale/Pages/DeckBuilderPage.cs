using Framework;
using Framework.Selenium;
using OpenQA.Selenium;

namespace Royale.Pages
{
    public class DeckBuilderPage : PageBase
    {
        public readonly DeckBuilderPageMap Map;

        public DeckBuilderPage()
        {
            Map = new DeckBuilderPageMap();
        }

        public DeckBuilderPage Goto()
        {
            HeaderNav.Map.DeckBuilderLink.Click();
            return this;
        }

        public void AddCardsManually()
        {
            Driver.Wait.Until(
                WaitConditions.ElementIsDisplayed(Map.AddCardsManuallyLink))
                .Click();
            Driver.Wait.Until(WaitConditions.ElementDisplayed(Map.CopyDeckIcon));
        }

        public void CopySuggestedDeck()
        {
            Map.CopyDeckIcon.Click();
        }
    }

    public class DeckBuilderPageMap
    {
        public Element AddCardsManuallyLink => Driver.FindElement(By.XPath("//a[text()='add cards manually']"), "Add Cards Manullay Link");

        public Element CopyDeckIcon => Driver.FindElement(By.CssSelector(".copyButton"), "Copy Deck Button");
    }
}
