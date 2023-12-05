using OpenQA.Selenium;

public class HomePage : BasePage{

    public HomePage(IWebDriver webDriver)
        : base(webDriver) {
        }

    public HomePage Open(){
        string url = ReadConfigFile()["url"];
        webDriver.Navigate().GoToUrl(url);
        webDriver.Manage().Window.Maximize();
        return this;

    }
    public Dictionary<string, string> ReadConfigFile()
    {
        var config = new Dictionary<string, string>();
        foreach (var row in File.ReadAllLines("/Users/karolina_kukula-biczewska/Documents/CourseBasic/AT_Course/TA_Task1/config.txt"))
            config.Add(row.Split('=')[0], string.Join("=", row.Split('=').Skip(1).ToArray()));
        return config;
    } 

    public HomePage AcceptAllCookies()
    {
        webDriver.FindElement(By.Id("onetrust-accept-btn-handler")).Click();
        return this;
    }

    public CareersPage ClickOnCareersLink()
    {
        webDriver.FindElement(By.LinkText("Careers")).Click();
        return  new CareersPage(webDriver);
    }

    public AboutPage ClickOnAboutLink()
    {
        webDriver.FindElement(By.LinkText("About")).Click();
        return new AboutPage(webDriver);
    }

    public InsightsPage ClickOnInsightsLink()
    {
        webDriver.FindElement(By.LinkText("Insights")).Click();
        return new InsightsPage(webDriver);
    }
    public HomePage ClickOnMagnifier()
    {
        webDriver.FindElement(By.ClassName("dark-iconheader-search__search-icon")).Click();
        return this;
    }

    public HomePage SearchText(string searchedText)
    {
        Thread.Sleep(2000);
        webDriver.FindElement(By.Name("q")).SendKeys(searchedText);
        return this;
    }
    public SearchResultPage ClickOnFindButton2()
    {
        webDriver.FindElement(By.ClassName("custom-search-button")).Click();
        return new SearchResultPage(webDriver);
    }
}