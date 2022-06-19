using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace WebsiteTestGlobal.pageObjects
{
    class CheckoutPage
    {
        private IWebDriver driver;

        public CheckoutPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #region selectors

        [FindsBy(How = How.Id, Using = "total_price")]
        [CacheLookup]
        public IWebElement totalPrice;

        #endregion selectors

        #region methods

        //Deletes an item from the cart based on index provided.
        public void deleteItemFromCart(int locationInCart)
        {
            driver.FindElement(By.XPath($"(//*[@class='cart_quantity_delete'])[{locationInCart}]")).Click();
            //Sleep is needed here as following procedures were occurring incorrectly as it takes a moment for the item to be removed from the cart.
            Thread.Sleep(3000);
        }

        //Increases the quantity of an item in the cart based on the index provided.
        public void increaseQuantityOfItemInCart(int locationInCart)
        {
            driver.FindElement(By.XPath($"(//*[@class='cart_quantity_up btn btn-default button-plus'])[{locationInCart}]")).Click();
            //Sleep is needed here as when getting the item total, the update had not finished completing yet.
            Thread.Sleep(3000);
        }

        //Gets the total price of an item from the cart based on the index provided.
        public string getItemTotal(int locationInCart)
        {
            var total = driver.FindElement(By.XPath($"(//*[@class='price'])[{locationInCart}]")).GetAttribute("textContent").Trim();

            return total;
        }

        // Gets the total cost of all items in the cart.
        public string getCartTotal()
        {
            return totalPrice.GetAttribute("textContent");
        }

        #endregion methods

    }
}
