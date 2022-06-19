using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace WebsiteTestGlobal.pageObjects
{
    class SearchResults
    {

        private IWebDriver driver;

        public SearchResults(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #region selectors

        //Header Logo on the top left of the page.
        [FindsBy(How = How.CssSelector, Using = "img[alt = 'My Store']")]
        [CacheLookup]
        private IWebElement headerLogo;

        #endregion selectors

        #region methods
        //Selects the item from the search results page based on the index provided.
        public ItemPage clickItemInSearchResults(int itemIndex)
        {
            driver.FindElement(By.CssSelector($"#center_column ul.product_list.grid.row li:nth-of-type({itemIndex}) div.product-container div:nth-of-type(2) h5 a.product-name")).Click();
            return new ItemPage(driver);
        }

        public LandingPage returnToLandingPage()
        {
            headerLogo.Click();
            return new LandingPage(driver);
        }

        #endregion methods
    }
}
