using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace Selenium1Example
{
    [TestClass]
    public class UnitTest1
    {
        private static readonly string DriverDirectory = "C:\\seleniumDrivers2";
        private static IWebDriver _driver;

        // https://www.automatetheplanet.com/mstest-cheat-sheet/
        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _driver = new ChromeDriver(DriverDirectory); // fast
            //_driver = new FirefoxDriver(DriverDirectory);  // slow
        }

        [ClassCleanup]
        public static void TearDown()
        {
            _driver.Dispose();
        }

        [TestMethod]
        public void TestMethodAnboEasj()
        {
            _driver.Navigate().GoToUrl("http://anbo-easj.dk/");
            string title = _driver.Title;
            Assert.AreEqual("Anders Børjesson", title);

            IList<IWebElement> elements = _driver.FindElements(By.ClassName("toptekst"));
            Assert.AreEqual(1, elements.Count);

            IWebElement anchorElement = _driver.FindElement(By.LinkText("CV"));
            anchorElement.Click();

            string title2 = _driver.Title;
            Assert.AreEqual("CV for Anders Børjesson", title2);
        }

        [TestMethod]
        public void TestYr() // Weather forecasts
        {
            _driver.Navigate().GoToUrl("https://www.yr.no/");
            string title = _driver.Title;
            Assert.IsTrue(title.StartsWith("Yr – ")); // long hyphen

            IWebElement inputField = _driver.FindElement(By.Id("sted"));
            inputField.SendKeys("Roskilde");
            inputField.Submit(); // press Return
            string title2 = _driver.Title;
            Assert.AreEqual("Søk: roskilde – Yr", title2); // long hyphen
        }

        [TestMethod]
        public void TestSayHello()
        {
            _driver.Navigate().GoToUrl("http://localhost:3004/");
            Assert.AreEqual("Say Hello", _driver.Title);
            IWebElement inputElement = _driver.FindElement(By.Id("inputField"));
            inputElement.SendKeys("Anders");

            IWebElement buttonElement = _driver.FindElement(By.Id("button"));
            buttonElement.Click();

            IWebElement resultElement = _driver.FindElement(By.Id("outputField"));
            string message = resultElement.Text;
            Assert.AreEqual("Hello Anders", message);
        }

        [TestMethod]
        public void TestSimpleCalculator()
        {
            //using (IWebDriver driver = new FirefoxDriver("C:\\seleniumDrivers2"))
            //{
            _driver.Navigate().GoToUrl("http://localhost:3006/");
            IWebElement inputElement1 = _driver.FindElement(By.Id("number1"));
            IWebElement inputElement2 = _driver.FindElement(By.Id("number2"));
            inputElement1.SendKeys("3");
            inputElement2.SendKeys("4");
            IWebElement buttonElement = _driver.FindElement(By.Id("calculateButton"));
            buttonElement.Click();
            IWebElement resultElement = _driver.FindElement(By.Id("result"));
            string result = resultElement.Text;
            Assert.AreEqual("7", result);
            //}
        }

        [TestMethod]
        public void TestAdvancedCalculator()
        {
            _driver.Navigate().GoToUrl("http://localhost:3006/");
            IWebElement inputElement1 = _driver.FindElement(By.Id("number1"));
            IWebElement inputElement2 = _driver.FindElement(By.Id("number2"));
            IWebElement selectElement = _driver.FindElement(By.Id("operation"));
            selectElement.SendKeys("-");

            inputElement1.SendKeys("3");
            inputElement2.SendKeys("4");
            IWebElement buttonElement = _driver.FindElement(By.Id("calculateButton"));
            buttonElement.Click();
            IWebElement resultElement = _driver.FindElement(By.Id("result"));
            string result = resultElement.Text;
            Assert.AreEqual("-1", result);
        }
    }
}
