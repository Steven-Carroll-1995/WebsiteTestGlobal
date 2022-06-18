using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace WebsiteTestGlobal.pageObjects
{
    class LandingPage
    {
        string url = "http://automationpractice.com/index.php";

        private IWebDriver driver;
        private WebDriverWait wait;

        public LandingPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
        }

        #region selectors

        [FindsBy(How = How.Id, Using = "search_query_top")]
        [CacheLookup]
        private IWebElement searchBar;

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
