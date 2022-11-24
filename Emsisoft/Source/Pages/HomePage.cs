using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Emsisoft.Source.Pages
{
    public class HomePage
    {
        private IWebDriver _driver;

        [FindsBy(How = How.LinkText, Using = "Alternative installation options")]
        private IWebElement _alternativeInstallationOptions;

        [FindsBy(How = How.LinkText, Using = "Web installer")]
        private IWebElement _webInstaller;

        public HomePage(IWebDriver driver) 
        { 
            _driver= driver;
            PageFactory.InitElements(driver, this);
        }

        public void alternateInstallation(string alternativeInstallationOptions)
        {
            _alternativeInstallationOptions.Click();
            _webInstaller.Click();
        }

        public void url()
        {
            string baseUrl = "https://www.emsisoft.com/en/anti-malware-home/";
            _driver.Navigate().GoToUrl(baseUrl);
        }
    }
}
