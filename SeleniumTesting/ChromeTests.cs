﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SeleniumTesting
{
    public class ChromeTests : IUseFixture<ChromeFixture>
    {
        ChromeDriver driver;

        public void SetFixture(ChromeFixture data)
        {
            driver = data.GetDriver();
        }

        [Fact]
        public void Google_com_should_return_search_results()
        {
            driver.Navigate().GoToUrl("http://www.google.com/ncr");
            driver.Manage().Window.Maximize();
            Task.Delay(TimeSpan.FromSeconds(5)).Wait();

            // here you can check HTML of the page you currently have loaded in the browser
            // and save it to the file
            File.WriteAllText("chrome-source-1.html", driver.PageSource);

            IWebElement query = driver.FindElement(By.Name("q"));
            query.SendKeys("Selenium");

            //var end = DateTime.Now.AddSeconds(5);
            //while(DateTime.Now < end)
            //{
            //    var resultsDiv = driver.FindElement(By.ClassName("sbdd_b"));
            //    if(resultsDiv.Displayed)
            //    {
            //        break;
            //    }
            //}

            //var allSuggestions = driver.FindElements(By.XPath("//div[@class='sbqs_c']"));
            //foreach(var suggestion in allSuggestions)
            //{
            //    Console.WriteLine(suggestion.Text);
            //}

            query.Submit();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until((d) => { return d.Title.StartsWith("Selenium"); });

            Assert.Equal("Selenium - Google Search", driver.Title);

            driver.GetScreenshot().SaveAsFile("chrome-snapshot.png", ScreenshotImageFormat.Png);
        }
    }

    public class ChromeFixture : IDisposable
    {
        ChromeDriver driver;

        public ChromeFixture()
        {
            driver = new ChromeDriver();
        }

        public ChromeDriver GetDriver()
        {
            return driver;
        }

        public void Dispose()
        {
            driver.Quit();
        }
    }
}
