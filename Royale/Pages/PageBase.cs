namespace Royale.Pages
{
    public abstract class PageBase
    {
        public readonly HeaderNav HeaderNav;

        public PageBase()
        {
            HeaderNav = new HeaderNav();
        }
    }
}
