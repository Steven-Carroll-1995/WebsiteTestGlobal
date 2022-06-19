using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace WebsiteTestGlobal.pageObjects
{
    class LandingPage
    {
        string url = "http://automationpractice.com/index.php";

        private IWebDriver driver;

        public LandingPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #region selectors

        // Search bar at the top of the screen.
        [FindsBy(How = How.Id, Using = "search_query_top")]
        [CacheLookup]
        private IWebElement searchBar;

        // Button needs to be clicked to kick off the search.
        [FindsBy(How = How.Name, Using = "submit_search")]
        [CacheLookup]
        private IWebElement searchButton;

        #endregion selectors

        #region methods

        // Navigates to the landing page of the test site
        public void goToPage()
        {
            driver.Navigate().GoToUrl(url);
        }

        // Performs a search using the provided search term
        public SearchResults search(string searchTerm)
        {
           searchBar.Clear();
           searchBar.SendKeys(searchTerm);
           searchButton.Click();
           return new SearchResults(driver);
        }

        #endregion methods

    }
}
