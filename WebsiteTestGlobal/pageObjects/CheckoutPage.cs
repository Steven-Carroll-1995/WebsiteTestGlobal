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
    class CheckoutPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public CheckoutPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
        }

        #region selectors

        [FindsBy(How = How.Id, Using = "total_price")]
        [CacheLookup]
        public IWebElement totalPrice;

        #endregion selectors

        #region methods

        public void deleteItemFromCart(int locationInCart)
        {
            driver.FindElement(By.XPath($"(//*[@class='cart_quantity_delete'])[{locationInCart}]")).Click();
            Thread.Sleep(3000);
        }

        public void increaseQuantityOfItemInCart(int locationInCart)
        {
            driver.FindElement(By.XPath($"(//*[@class='cart_quantity_up btn btn-default button-plus'])[{locationInCart}]")).Click();
            Thread.Sleep(3000);
        }

        public string getItemTotal(int locationInCart)
        {
            var total = driver.FindElement(By.XPath($"(//*[@class='price'])[{locationInCart}]")).GetAttribute("textContent").Trim();

            return total;
        }

        public string getCartTotal()
        {
            return totalPrice.GetAttribute("textContent");
        }

        #endregion methods

    }
}
