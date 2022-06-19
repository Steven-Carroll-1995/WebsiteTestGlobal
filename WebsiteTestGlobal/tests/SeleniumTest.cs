using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebsiteTestGlobal.pageObjects;

namespace WebsiteTestGlobal.tests
{

    public class Tests

    {

        IWebDriver driver;

        [OneTimeSetUp]

        public void Setup()

        {

            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            //Creates the ChomeDriver object, Executes tests on Google Chrome

            driver = new ChromeDriver(path + @"\drivers\");

        }

        #region tests

        [Test]
        [Description(@"GIVEN I am on the automation practice site
                       WHEN I add a Faded Short Sleeve T-shirt, Evening Dress, and Printed Summer Dress to my cart
                       AND I have removed the Evening Dress from my cart
                       AND I have increased the quantity of my Faded Short Sleeve T-shirts
                       THEN the item totals and cart totals are as expected")]
        public void ExampleOrder()
        {
            LandingPage landingPage = new LandingPage(driver);

            landingPage.goToPage();

            //Adding required items to the cart.
            var itemPage = addItemToCart(landingPage, "Faded Short Sleeve T-shirts", 1, 1, "M", "Blue");
            itemPage = addItemToCart(itemPage, "Evening Dress", 1, 1, "S", "Beige");
            itemPage = addItemToCart(itemPage, "Printed Summer Dress", 1, 1, "M", "Orange");

            // Going to the checkout page.
            var checkout = itemPage.proceedToCheckout();

            //Deleting the Evening Dress from the cart
            checkout.deleteItemFromCart(2);

            // Adding another Faded Short Sleeve T-shirt
            checkout.increaseQuantityOfItemInCart(1);

            Assert.That(checkout.getItemTotal(1), Is.EqualTo("$33.02"));
            Assert.That(checkout.getItemTotal(2), Is.EqualTo("$28.98"));
            Assert.That(checkout.getCartTotal(), Is.EqualTo("$65.53"),"The cart total did not match the intended total. Please ensure that the individual item prices are correct.");
        }

        #endregion tests



        [OneTimeTearDown]

        public void TearDown()

        {

            driver.Quit();

        }

        #region methods

        /**
         * This method takes in a landing page and then searches using a provided search term.
         * It then selects an item from the search results.
         * It then configures the quantity, size and colour of the item.
         * It then adds it to the cart.
         */
        private ItemPage addItemToCart(LandingPage landingPage, string searchTerm, int resultsIndex, int quantity, string size, string colour)
        {
            var searchResults = landingPage.search(searchTerm);
            var itemPage = searchResults.clickItemInSearchResults(resultsIndex);
            itemPage.setQuantity(quantity);
            itemPage.setSize(size);
            itemPage.setColour(colour);
            itemPage.addItemToCart();
            return itemPage;
        }

        /**
         * This method takes in a item page and then searches using a provided search term.
         * It then selects an item from the search results.
         * It then configures the quantity, size and colour of the item.
         * It then adds it to the cart.
         */
        private ItemPage addItemToCart(ItemPage oldItem, string searchTerm, int resultsIndex, int quantity, string size, string colour)
        {
            var searchResults = oldItem.search(searchTerm);
            var newItem = searchResults.clickItemInSearchResults(resultsIndex);
            newItem.setQuantity(quantity);
            newItem.setSize(size);
            newItem.setColour(colour);
            newItem.addItemToCart();
            return newItem;
        }

        #endregion methods

    }

}