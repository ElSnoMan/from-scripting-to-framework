using System;
using Framework.Selenium;

namespace Royale.Pages
{
    public class Pages
    {
        [ThreadStatic]
        public static CardsPage Cards;

        [ThreadStatic]
        public static CardDetailsPage CardDetails;

        public static void Init()
        {
            Cards = new CardsPage();
            CardDetails = new CardDetailsPage();
        }
    }
}
