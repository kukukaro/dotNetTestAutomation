using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;

public class CareersPage : BasePage {
    public CareersPage(IWebDriver webDriver)
        : base(webDriver) {
        }
        
    public CareersPage InputToField(string skill)
    {
        webDriver.FindElement(By.XPath("//input[@id='new_form_job_search-keyword']"))
        .SendKeys(skill);
        return this;
    }

    public CareersPage ChooseFromDropDownList(string location)
    {
        webDriver.FindElement(By.CssSelector("div.recruiting-search__location")).Click();
        webDriver.FindElement(By.ClassName("select2-selection--single")).SendKeys(" ");

        webDriver.Manage()
                 .Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        string xPathLocation = "//li[@title='" + location + "']";
        webDriver.FindElement(By.XPath(xPathLocation))
                 .Click();
        return this;
    }

    public CareersPage CheckRemoteCheckBox()
    {
        webDriver.FindElement(By.ClassName("job-search__filter-items--remote"))
                 .Click();
        return this;
    }

    public CareersPage ClickOnFindButton()
    {
        webDriver.FindElement(By.XPath("//button[@type='submit']"))
                 .Click();
        return this;
    }
    public CareersPage OpenTheLastOffer()
    {
        var prevElements = 0;
        var elements = webDriver.FindElements(By.CssSelector(".search-result__item")).Count;

        while (prevElements != elements)
        {
            webDriver.ExecuteJavaScript("window.scrollTo(0, document.body.scrollHeight)");
            Thread.Sleep(5000);
            prevElements = elements;
            elements = webDriver.FindElements(By.CssSelector(".search-result__item")).Count;
        }

        Actions actions = new Actions(webDriver);
        actions.MoveToElement(webDriver.FindElement(By.XPath("//li[@class='search-result__item'][last()]//a[contains(text(),\"View\")]")))
            .Click().Perform();
        
        return this;
    }

    public IWebElement GetText(string skill)
    {
        string XPath = "//*[not(self::script)][contains(text(),\"" + skill + "\")]";
        IWebElement text = webDriver.FindElement(By.XPath(XPath));
        return text;
    }
}