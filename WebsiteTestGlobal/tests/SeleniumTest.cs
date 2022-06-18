using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using WebsiteTestGlobal.pageObjects;
using System;
using System.Collections.ObjectModel;
using System.IO;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace WebsiteTestGlobal.tests
{

    public class Tests

    {

        IWebDriver driver;

        [OneTimeSetUp]

        public void Setup()

        {

            //Below code is to get the drivers folder path dynamically.

            //You can also specify chromedriver.exe path dircly ex: C:/MyProject/Project/drivers

            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            //Creates the ChomeDriver object, Executes tests on Google Chrome

            driver = new ChromeDriver(path + @"\drivers\");

        }

        [Test]
        public void ExampleOrder()
        {
            LandingPage landingPage = new LandingPage(driver);

            landingPage.goToPage();
            var searchResults = landingPage.search("Faded Short Sleeve T-shirts");
            var itemPage = searchResults.clickItemInSearchResults(1);
            itemPage.setQuantity(1);
            itemPage.setSize("M");
            itemPage.setColour("Blue");
            itemPage.addItemToCart();
            searchResults = itemPage.search("Evening Dress");
            itemPage = searchResults.clickItemInSearchResults(1);
            itemPage.setQuantity(1);
            itemPage.setColour("Beige");
            itemPage.setSize("S");
            itemPage.addItemToCart();
            searchResults = itemPage.search("Printed Summer Dress");
            itemPage = searchResults.clickItemInSearchResults(1);
            itemPage.setQuantity(1);
            itemPage.setColour("Orange");
            itemPage.setSize("M");
            itemPage.addItemToCart();
            var checkout = itemPage.proceedToCheckout();
            checkout.deleteItemFromCart(2);
            checkout.increaseQuantityOfItemInCart(1);
            Assert.That(checkout.getItemTotal(1), Is.EqualTo("$33.02"));
            Assert.That(checkout.getItemTotal(2), Is.EqualTo("$28.98"));
            Assert.That(checkout.getCartTotal(), Is.EqualTo("$65.53"),"The cart total did not match the intended total. Please ensure that the individual item prices are correct.");
        }

        [OneTimeTearDown]

        public void TearDown()

        {

            driver.Quit();

        }

    }

}