using Emsisoft.Source.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Emsisoft.Tests
{
    public class HomeTest
    {
        private IWebDriver _driver;
        [SetUp]
        public void InitScript() 
        {
            string path = System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Downloads";
            ChromeOptions option = new ChromeOptions();
            option.AddArgument("--headless");
            option.AddUserProfilePreference("download.default_directory", path);
            option.AddUserProfilePreference("download.prompt_for_download", false);
            option.AddUserProfilePreference("disable-popup-blocking", "true");
            option.AddUserProfilePreference("safebrowsing.enabled", "true");
            _driver = new ChromeDriver(option);
        }

        [Test]
        public void downloadWebInstaller() 
        {
            string expectedFilePath = System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Downloads\\EmsisoftAntiMalwareWebSetup.exe";
            bool fileExists = false;
            HomePage hp = new HomePage(_driver);
            hp.url();
            hp.alternateInstallation("webdriver download");
            Assert.True(_driver.Title.Contains("Download and installation"));

            try 
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                wait.Until<bool>(x => fileExists = File.Exists(expectedFilePath));

                FileInfo fileInfo = new FileInfo(expectedFilePath);

                Assert.AreEqual(fileInfo.Name, "EmsisoftAntiMalwareWebSetup.exe");
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
            }
            finally 
            {
                if(File.Exists(expectedFilePath))
                    File.Delete(expectedFilePath);
            }
        }

        [TearDown]
        public void Cleanup() 
        {
            _driver.Quit();
        }
    }
}
