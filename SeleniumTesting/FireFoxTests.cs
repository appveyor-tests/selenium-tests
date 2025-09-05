using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
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
    public class FireFoxTests : IUseFixture<FireFoxFixture>
    {
        FirefoxDriver driver;

        public void SetFixture(FireFoxFixture data)
        {
            driver = data.GetDriver();
        }

        [Fact]
        public void Google_com_should_return_search_results()
        {
            driver.Navigate().GoToUrl("http://www.google.com/ncr");
            IWebElement query = driver.GetElement(By.Name("q"));
            query.SendKeys("Selenium");
            query.Submit();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            //wait.Until((d) => { return d.Title.StartsWith("Selenium"); });
            Console.WriteLine("Current title: " + driver.Title);
            Console.WriteLine(driver.PageSource);
            wait.Until((d) => { return d.Title.Contains("Selenium") || d.Url.Contains("Selenium"); });

            Assert.Equal("Selenium - Google Search", driver.Title);
            
            Task.Delay(1000).Wait();

            driver.GetScreenshot().SaveAsFile("firefox-snapshot.png");
        }
    }

    public class FireFoxFixture : IDisposable
    {
        FirefoxDriver driver;

        public FireFoxFixture()
        {
            //Environment.SetEnvironmentVariable("webdriver.log.file", "log-file.txt");
            //Environment.SetEnvironmentVariable("webdriver.firefox.logfile", "ff-log.txt");
            //driver = new FirefoxDriver(new FirefoxOptions());
            var driverService = FirefoxDriverService.CreateDefaultService();
            driverService.FirefoxBinaryPath = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
            if(!File.Exists(driverService.FirefoxBinaryPath))
            {
                driverService.FirefoxBinaryPath = @"C:\Program Files\Mozilla Firefox\firefox.exe";
            }
            driverService.HideCommandPromptWindow = true;
            driverService.SuppressInitialDiagnosticInformation = true;
            
            driver = new FirefoxDriver(driverService, new FirefoxOptions(), TimeSpan.FromSeconds(60));            
        }

        public FirefoxDriver GetDriver()
        {
            return driver;
        }

        public void Dispose()
        {
            driver.Quit();
        }
    }
}
