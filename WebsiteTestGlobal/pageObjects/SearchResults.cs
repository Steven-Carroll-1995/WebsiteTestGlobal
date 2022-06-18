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
    class SearchResults
    {

        private IWebDriver driver;
        private WebDriverWait wait;

        public SearchResults(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
        }

        #region selectors

        [FindsBy(How = How.CssSelector, Using = "img[alt = 'My Store']")]
        [CacheLookup]
        private IWebElement headerLogo;

        #endregion selectors

        #region methods
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
