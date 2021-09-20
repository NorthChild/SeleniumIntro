using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Threading;

namespace SeleniumIntro
{
    public class Tests
    {

        /// <summary>
        /// /////////////////////////////////////////////////// AUTOMATIONPRACTICE /// ///////////////////////////////////////////////////
        /// </summary>
        
        [Test]
        public void GivenIAmOnTheHomePage_WhenIClickTheSignInLink_ThenIGoToTheSigninPage()
        {

            using (IWebDriver driver = new ChromeDriver())
            {

                // Maximise browser
                driver.Manage().Window.FullScreen();

                // Navigate to the API site
                driver.Navigate().GoToUrl("http://automationpractice.com/index.php");

                // Grab sign in link so we can interact with it
                IWebElement signInLink = driver.FindElement(By.LinkText("Sign in"));
                
                // Act - click sign in Link
                signInLink.Click();

                // Wait to ensure a resopsone
                Thread.Sleep(5000);

                // Assert - that we are now on the sign in age
                Assert.That(driver.Title, Is.EqualTo("Login - My Store"));

            }

        }

        [Test]
        public void GivenIAmOnTheSignInPage_AndIEnterA4DigitPassword_WhenIClickTheSignInButton_TheIGetAnErrorMessage()
        {

            using (IWebDriver driver = new ChromeDriver())
            {

                // Maximise browser
                driver.Manage().Window.FullScreen();

                // Navigate to the API site
                driver.Navigate().GoToUrl("http://automationpractice.com/index.php?controller=authentication&back=my-account");

                // Grab the email input field
                IWebElement emailField = driver.FindElement(By.Id("email"));

                // Enter a valid Email
                emailField.SendKeys("testing@snailMail.ccm");

                // find password field
                IWebElement passField = driver.FindElement(By.Id("passwd"));

                // enter a password with less than 5 characters
                passField.SendKeys("1234");

                // get sign in button
                var signInButton = driver.FindElement(By.Id("SubmitLogin"));

                // click login button
                signInButton.Click();

                // find the alert element
                var alertElement = driver.FindElement(By.ClassName("alert"));

                // ASSERT - correct error message is displayed
                Assert.That(alertElement.Text.Contains("Invalid password"));


            }
        }

        /// <summary>
        /// /////////////////////////////////////////////////// JOHN LEWIS /// ///////////////////////////////////////////////////
        /// </summary>

        [Test]
        public void UsingMouseHoverAction_OnJohnLewis()
        {

            using (IWebDriver driver = new ChromeDriver())
            {

                // Maximise browser
                driver.Manage().Window.FullScreen();

                // Navigate to the API site
                driver.Navigate().GoToUrl("http://johnlewis.com");

                // wait for response
                Thread.Sleep(2000);

                // Find "accept cookies bttn" and click
                var acceptAllCookiesButton = driver.FindElement(By.ClassName("c-button-yMKB7"));
                acceptAllCookiesButton.Click();

                // find home and garden element
                var homeAndGardenElement = driver.FindElement(By.LinkText("Home & Garden"));

                // instantiate an action which we can use for more complex websites
                Actions action = new Actions(driver);

                // get mouse to hover over the Home and garden element
                action.MoveToElement(homeAndGardenElement).Perform();

                Thread.Sleep(5000);

                // grab beddin link
                var beddingsLink = driver.FindElement(By.LinkText("Bedding"));
                beddingsLink.Click();

                // check page title is correct
                Assert.That(driver.Title.Contains("Bedding | Bed Sets and Bed Linen"));
            }
        }


        /// <summary>
        /// /////////////////////////////////////////////////// COMPUTERHOPE /// ///////////////////////////////////////////////////
        /// </summary>

        [Test]
        public void HandlingMultipleWindows()
        {

            using (IWebDriver driver = new ChromeDriver())
            {

                // Maximise browser
                driver.Manage().Window.FullScreen();

                // Navigate to the API site
                driver.Navigate().GoToUrl("https://www.computerhope.com/");

                // wait for response
                Thread.Sleep(2000);

                // 
                var twitterLink = driver.FindElement(By.LinkText("Twitter"));
                twitterLink.Click();

                driver.SwitchTo().Window(driver.WindowHandles[1]);

                Thread.Sleep(5000);
                driver.Close();

                driver.SwitchTo().Window(driver.WindowHandles[0]);
                Assert.That(driver.Title, Is.EqualTo("Computer Hope's Free Computer Help"));

                var helpLink = driver.FindElement(By.LinkText("Help"));
                helpLink.Click();

                Assert.That(driver.Title, Is.EqualTo("Computer Online Help"));

                driver.Navigate().Back();

                Assert.That(driver.Title, Is.EqualTo("Computer Hope's Free Computer Help"));
            }
        }


        [Test]
        public void EndToEndUserExperienceTest_FromMain_ToDictionary_ToSpecificIndex_ToSearchBar()
        {

            using (IWebDriver driver = new ChromeDriver())
            {

                // Maximise browser
                driver.Manage().Window.FullScreen();

                // Navigate to the API site
                driver.Navigate().GoToUrl("https://www.computerhope.com/");

                // wait for response
                Thread.Sleep(2000);

                // find and click dictionary button
                var twitterLink = driver.FindElement(By.LinkText("Dictionary"));
                twitterLink.Click();

                Thread.Sleep(2000);

                // check we are on the right page
                Assert.That(driver.Url.Contains("https://www.computerhope.com/jargon.htm"));

                Thread.Sleep(2000);

                // find and click A button in 0-9 a/z glossary
                var aGlossaryLink = driver.FindElement(By.LinkText("A"));
                aGlossaryLink.Click();

                Thread.Sleep(2000);

                // check we are on the right page
                Assert.That(driver.Title.Contains("A - Computer Dictionary and Glossary"));
                Assert.That(driver.Url.Contains("https://www.computerhope.com/jargon/ja.htm"));

                // insert in search field item to search
                var searchBar = driver.FindElement(By.ClassName("sbar"));
                searchBar.SendKeys("async");

                ////click to search
                var searchButton = driver.FindElement(By.XPath("//button[@type='Submit']"));
                searchButton.Click();

                // check we are on the right page
                Assert.That(driver.Title.Contains("What is Asynchronous?"));
                Assert.That(driver.Url.Contains("https://www.computerhope.com/jargon/a/asynchro.htm"));

                Thread.Sleep(2000);
            }
        }





    }
} 