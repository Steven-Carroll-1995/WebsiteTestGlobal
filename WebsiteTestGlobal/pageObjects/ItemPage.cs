using OpenQA.Selenium;
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

        //Quantity of Item
        [FindsBy(How = How.Id, Using = "quantity_wanted")]
        [CacheLookup]
        private IWebElement quantity;

        /**
         * Size of the item - Has three values:
         * - S
         * - M
         * - L
         */
        [FindsBy(How = How.Id, Using = "group_1")]
        [CacheLookup]
        private IWebElement size;

        // Add to cart button
        [FindsBy(How = How.Name, Using = "Submit")]
        [CacheLookup]
        public IWebElement addToCart;

        //Search bar at the top of the screen.
        [FindsBy(How = How.Id, Using = "search_query_top")]
        [CacheLookup]
        private IWebElement searchBar;

        // Search button to kick off the search.
        [FindsBy(How = How.Name, Using = "submit_search")]
        [CacheLookup]
        private IWebElement searchButton;

        /**
         * This button appears in a pop-up after an item is added to the cart.
         * Clicking it dismisses the pop-up and returns to the item page.
         */
        [FindsBy(How = How.CssSelector, Using = "span[title='Continue shopping']")]
        [CacheLookup]
        private IWebElement continueShopping;

        // Navigates to the checkout page when clicked.
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
            //Wait is needed here as the element to be clicked appears in a pop-up which takes a few moments to be visible.
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
