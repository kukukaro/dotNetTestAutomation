using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;

public class SearchResultPage : BasePage {

    public SearchResultPage(IWebDriver webDriver)
        : base(webDriver) {
        }

    
    public SearchResultPage LoadAllResults()
    {
        //scroll to bottom, as second page of results got loaded automatically
        webDriver.ExecuteJavaScript("window.scrollTo(0, document.body.scrollHeight)");
        Thread.Sleep(1000);

        Actions actions = new Actions(webDriver);

        while (ViewMoreExist())
        {
            actions.MoveToElement(webDriver.FindElement(By.CssSelector("a.search-results__view-more")))
            .Click()
            .Perform();
            Thread.Sleep(1000);
        }

        return this;
    }
    private bool ViewMoreExist()
    {
        //element is always present in the page source, but got hidden when no longer needed
        var element = webDriver.FindElement(By.CssSelector("a.search-results__view-more"));
        return element.Enabled && element.Displayed;
    }

    public ReadOnlyCollection<IWebElement> ValidateLinks()
    {
        return webDriver.FindElements(By.XPath("//h3[@class='search-results__title']/a"));
    }
}