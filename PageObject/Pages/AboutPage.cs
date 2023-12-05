using OpenQA.Selenium;

public class AboutPage : BasePage {
    public AboutPage(IWebDriver webDriver)
        : base(webDriver) {
        }

    public AboutPage ScrollToEpamAtaGlance(){
        IWebElement epamAtaGlance = webDriver.FindElement(By.XPath("//*[contains(text(),\"EPAM at\")]"));
        new OpenQA.Selenium.Interactions.Actions(webDriver)
                .ScrollToElement(epamAtaGlance)
                .Perform();
        return this;
    }

    public AboutPage ClickOnDownloadButton(){
        webDriver.FindElement(By.XPath("//*[contains(text(),\"DOWNLOAD\")]")).Click();
        return this;
    }

    public static bool CheckFile(String path){
        
        return false;
    }
}