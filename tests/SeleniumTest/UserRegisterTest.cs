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
    public class UserRegisterTest
    {
        string _baseURL = string.Empty;
        IWebDriver _driver;
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Setup()
        {
            _driver = new FirefoxDriver();
            var webAppUrl = TestContext.Properties["webRegisterUrl"].ToString();
            this._baseURL = webAppUrl;
            //this._baseURL = @"http://3.16.83.234:32651/Identity/Account/Register?returnUrl=%2F";
            //_driver = new ChromeDriver(Environment.GetEnvironmentVariable("ChromeWebDriver"));
            
        }

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

        [TestMethod]
        public void ClickRegisterButtonWithAllBlankField()
        {
            this._driver.Navigate().GoToUrl(this._baseURL);
            this._driver.FindElement(By.Id("Input_Email")).SendKeys(string.Empty);
            this._driver.FindElement(By.Id("Input_Password")).SendKeys(string.Empty);
            this._driver.FindElement(By.Id("Input_ConfirmPassword")).SendKeys(string.Empty);
            this._driver.FindElement(By.XPath("//*[contains(@class,'btn')]")).Click();
            string errorText = this._driver.FindElement(By.ClassName("validation-summary-errors")).Text.Replace(System.Environment.NewLine, string.Empty);
            Assert.AreEqual(@"The Email field is required.The Password field is required.", errorText);
            //PushScreenshot();
        }

        [TestMethod]
        public void ClickRegisterButtonWithUserNameBlank()
        {
            this._driver.Navigate().GoToUrl(this._baseURL);
            this._driver.FindElement(By.Id("Input_Email")).SendKeys(string.Empty);
            this._driver.FindElement(By.Id("Input_Password")).SendKeys("testpassword");
            this._driver.FindElement(By.Id("Input_ConfirmPassword")).SendKeys("testpassword");
            this._driver.FindElement(By.XPath("//*[contains(@class,'btn')]")).Click();
            string errorText = this._driver.FindElement(By.ClassName("validation-summary-errors")).Text.Replace(System.Environment.NewLine, string.Empty);
            Assert.AreEqual(@"The Email field is required.", errorText);
            //PushScreenshot();
        }

        //[TestMethod]
        //public void ClickRegisterButtonWithPasswordAndConfirmPasswrdBlank()
        //{
        //    this._driver.Navigate().GoToUrl(this._baseURL);
        //    this._driver.FindElement(By.Id("Input_Email")).SendKeys("testusername");
        //    this._driver.FindElement(By.Id("Input_Password")).SendKeys(string.Empty);
        //    this._driver.FindElement(By.Id("Input_ConfirmPassword")).SendKeys(string.Empty);
        //    this._driver.FindElement(By.XPath("//*[contains(@class,'btn')]")).Click();
        //    string errorText = this._driver.FindElement(By.ClassName("validation-summary-errors")).Text.Replace(System.Environment.NewLine, string.Empty);
        //    Assert.AreEqual("The Password field is required.", errorText);
        //    PushScreenshot();
        //}

        //[TestMethod]
        //public void ClickRegisterWithPasswordMismatch()
        //{
        //    this._driver.Navigate().GoToUrl(this._baseURL);
        //    this._driver.FindElement(By.Id("Input_Email")).SendKeys("testusername");
        //    this._driver.FindElement(By.Id("Input_Password")).SendKeys("password1");
        //    this._driver.FindElement(By.Id("Input_ConfirmPassword")).SendKeys("password2");
        //    //this._driver.FindElement(By.XPath(".//*[@value='Register']")).Click();
        //    this._driver.FindElement(By.XPath("//*[contains(@class,'btn')]")).Click();
        //    string errorText = this._driver.FindElement(By.ClassName("validation-summary-errors")).Text.Replace(System.Environment.NewLine, string.Empty);
        //    Assert.AreEqual("'Confirm password' and 'Password' do not match.", errorText);
        //    PushScreenshot();
        //}

        //[TestMethod]
        //public void ClickRegisterWithMinimalPasswordLengthVioalationRule()
        //{
        //    this._driver.Navigate().GoToUrl(this._baseURL);
        //    this._driver.FindElement(By.Id("Input_Email")).SendKeys("testusername");
        //    this._driver.FindElement(By.Id("Input_Password")).SendKeys("123");
        //    this._driver.FindElement(By.Id("Input_ConfirmPassword")).SendKeys("123");
        //    //this._driver.FindElement(By.XPath(".//*[@value='Register']")).Click();
        //    this._driver.FindElement(By.XPath("//*[contains(@class,'btn')]")).Click();
        //    string errorText = this._driver.FindElement(By.ClassName("validation-summary-errors")).Text.Replace(System.Environment.NewLine, string.Empty);
        //    Assert.AreEqual("The Password must be at least 6 characters long.", errorText);
        //    PushScreenshot();
        //}

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
