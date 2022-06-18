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
using SeleniumExtras.WaitHelpers;

namespace WebsiteTestGlobal.pageObjects
{
    class ItemPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public ItemPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
        }

        #region selectors

        [FindsBy(How = How.Id, Using = "quantity_wanted")]
        [CacheLookup]
        private IWebElement quantity;

        [FindsBy(How = How.Id, Using = "group_1")]
        [CacheLookup]
        private IWebElement size;

        [FindsBy(How = How.Name, Using = "Submit")]
        [CacheLookup]
        public IWebElement addToCart;

        [FindsBy(How = How.Id, Using = "search_query_top")]
        [CacheLookup]
        private IWebElement searchBar;

        [FindsBy(How = How.Name, Using = "submit_search")]
        [CacheLookup]
        private IWebElement searchButton;

        [FindsBy(How = How.CssSelector, Using = "span[title='Continue shopping']")]
        [CacheLookup]
        private IWebElement continueShopping;

        [FindsBy(How = How.CssSelector, Using = "a[title='View my shopping cart']")]
        [CacheLookup]
        private IWebElement checkout;

        #endregion selectors

        #region methods

        public void setQuantity(int quantityValue)
        {
            quantity.Clear();
            quantity.SendKeys(quantityValue.ToString());
        }

        public void setSize(string sizeValue)
        {
            new SelectElement(size).SelectByText(sizeValue);
        }

        public void setColour(string colour)
        {
            driver.FindElement(By.Name(colour)).Click();
        }

        public void addItemToCart()
        {
            addToCart.Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(continueShopping));
            continueShopping.Click();
        }

        // Performs a search using the provided search term
        public SearchResults search(string searchTerm)
        {
            searchBar.Clear();
            searchBar.SendKeys(searchTerm);
            searchButton.Click();
            return new SearchResults(driver);
        }

        public CheckoutPage proceedToCheckout()
        {
            checkout.Click();
            return new CheckoutPage(driver);
        }

        #endregion methods

    }
}
