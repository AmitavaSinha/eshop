using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System.IO;
using OpenQA.Selenium.Firefox;


namespace SeleniumTest
{
    [TestClass]
    public class LoginTest
    {
        string _baseURL = string.Empty;
        IWebDriver _driver;
        public TestContext TestContext { get; set; }


        //private RemoteWebDriver GetChromeDriver()
        //{
        //    var path = Environment.GetEnvironmentVariable("ChromeWebDriver");
        //    //var path = @"C:\SeleniumDriver";
        //    var options = new ChromeOptions();
        //    options.AddArguments("--no-sandbox");

        //    if (!string.IsNullOrWhiteSpace(path))
        //    {
        //        return new ChromeDriver(path, options, TimeSpan.FromSeconds(300));
        //    }
        //    else
        //    {
        //        return new ChromeDriver(options);
        //    }
        //}

        [TestInitialize]
        public void Setup()
        {
            _driver = new FirefoxDriver();
            var webAppUrl = TestContext.Properties["webLoginUrl"].ToString();
            this._baseURL = webAppUrl;
            //this._baseURL = @"http://104.208.160.159/Identity/Account/Login";
            
        }

        [TestMethod]
        public void ClickLoginButtonWithoutUserName()
        {
            this._driver.Navigate().GoToUrl(this._baseURL);
            this._driver.FindElement(By.Id("Input_Email")).SendKeys(string.Empty);
            this._driver.FindElement(By.Id("Input_Password")).SendKeys("SomePassword");
            //this._driver.FindElement(By.XPath("//input[@ type='submit'")).Click();
            //this._driver.FindElement(By.XPath(".//*[@value='Log in']")).Click();
            this._driver.FindElement(By.XPath("//*[contains(@class,'btn')]")).Click();
            string errorText = this._driver.FindElement(By.ClassName("validation-summary-errors")).Text.Replace(System.Environment.NewLine, string.Empty);
            Assert.AreEqual("The Email field is required.", errorText);
            //PushScreenshot();
        }

        [TestMethod]
        public void ClickLoginButtonWithoutPassword()
        {
            this._driver.Navigate().GoToUrl(this._baseURL);
            this._driver.FindElement(By.Id("Input_Email")).SendKeys("foo@bar.com");
            this._driver.FindElement(By.Id("Input_Password")).SendKeys(string.Empty);
            this._driver.FindElement(By.XPath("//*[contains(@class,'btn')]")).Click();
            //string errorText = this._driver.FindElement(By.XPath(".//span[@for='Input_Password']")).Text;
            string errorText = this._driver.FindElement(By.ClassName("validation-summary-errors")).Text.Replace(System.Environment.NewLine, string.Empty);
            Assert.AreEqual("The Password field is required.", errorText);
            //Assert.AreEqual(string.Empty, errorText); 
            //PushScreenshot();
        }



        [TestMethod]
        public void ClickLogniButtonWithoutUserNameAndPassword()
        {
            this._driver.Navigate().GoToUrl(this._baseURL);
            this._driver.FindElement(By.Id("Input_Email")).SendKeys(string.Empty);
            this._driver.FindElement(By.Id("Input_Password")).SendKeys(string.Empty);
            this._driver.FindElement(By.XPath("//*[contains(@class,'btn')]")).Click();
            string errorText = this._driver.FindElement(By.ClassName("validation-summary-errors")).Text.Replace(System.Environment.NewLine, string.Empty);
            Assert.AreEqual("The Email field is required.The Password field is required.", errorText);
            //PushScreenshot();
        }


        //public void PushScreenshot()
        //{
        //    var filePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()) + ".png";
        //    var screenshot = this._driver.GetScreenshot();
        //    screenshot.SaveAsFile(filePath);
        //    TestContext.AddResultFile(filePath);
        //}

        [TestCleanup]
        public void TearDown()
        {
            this._driver.Close();
            this._driver.Dispose();
        }
    }
}
