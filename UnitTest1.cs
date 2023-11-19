using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using NUnit.Framework.Internal;

namespace TA_Task1;

public class Tests
{
    IWebDriver webDriver;

    [SetUp]
    public void Setup()
    {
        webDriver = new ChromeDriver();
    }

    [TearDown]
    public void CleanUp()
    {
        webDriver.Quit();
    }

    [Test]
    [TestCase("BLOCKCHAIN")]
    [TestCase("Cloud")]
    [TestCase("Automation")]
    public void ValidateIfGlobalSearchWorksAsExpected(string searchedText)
    {
        string url = ReadConfigFile()["url"];
        webDriver.Navigate().GoToUrl(url);
        webDriver.Manage().Window.Maximize();
        AcceptAllCookies();
        ClickOnMagnifier();
        SearchText(searchedText);
        ClickOnFindButton2();
        LoadAllResults();
        ValidateLinks(searchedText);
    }

    [Test]
    [TestCase("C#", "All Locations")]
    public void Test1ValidateThatTheUserCanSearchForPositionBasedOnCriteria(string skill, string location)
    {
        string url = ReadConfigFile()["url"];
        webDriver.Navigate().GoToUrl(url);
        webDriver.Manage().Window.Maximize();
        AcceptAllCookies();
        ClickOnLink();
        InputToField(skill);
        ChooseFromDropDownList(location);
        CheckRemoteCheckBox();
        ClickOnFindButton();
        OpenTheLastOffer();
        CheckText(skill);
    }



    private Dictionary<string, string> ReadConfigFile()
    {
        var config = new Dictionary<string, string>();
        foreach (var row in File.ReadAllLines("/Users/karolina_kukula-biczewska/Documents/CourseBasic/AT_Course/TA_Task1/config.txt"))
            config.Add(row.Split('=')[0], string.Join("=", row.Split('=').Skip(1).ToArray()));
        return config;
    }

    private void AcceptAllCookies()
    {
        webDriver.FindElement(By.Id("onetrust-accept-btn-handler")).Click();
    }

    private void ClickOnLink()
    {
        webDriver.FindElement(By.LinkText("Careers")).Click();
    }

    private void InputToField(string skill)
    {
        webDriver.FindElement(By.XPath("//input[@id='new_form_job_search-keyword']")).SendKeys(skill);
    }

    private void ChooseFromDropDownList(string location)
    {
        webDriver.FindElement(By.CssSelector("div.recruiting-search__location")).Click();
        webDriver.FindElement(By.ClassName("select2-selection--single")).SendKeys(" ");

        webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        string xPathLocation = "//li[@title='" + location + "']";
        webDriver.FindElement(By.XPath(xPathLocation)).Click();
    }

    private void CheckRemoteCheckBox()
    {
        webDriver.FindElement(By.ClassName("job-search__filter-items--remote")).Click();
    }

    private void ClickOnFindButton()
    {
        webDriver.FindElement(By.XPath("//button[@type='submit']")).Click();
    }

    private void OpenTheLastOffer()
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
    }

    private void CheckText(string skill)
    {
        string XPath = "//*[not(self::script)][contains(text(),\"" + skill + "\")]";
        IWebElement text = webDriver.FindElement(By.XPath(XPath));

        Assert.IsTrue(text.Text.Contains(skill));
    }

    private void ClickOnMagnifier()
    {
        webDriver.FindElement(By.ClassName("dark-iconheader-search__search-icon")).Click();
    }

    private void ClickOnFindButton2()
    {
        webDriver.FindElement(By.ClassName("custom-search-button")).Click();
    }

    private void SearchText(string searchedText)
    {
        Thread.Sleep(2000);
        webDriver.FindElement(By.Name("q")).SendKeys(searchedText);
        //webDriver.FindElement(By.XPath("//input[@id='new_form_search']")).SendKeys(searchedText);
    }

    private void LoadAllResults()
    {
        //scroll to bottom, as second page of results got loaded automatically
        webDriver.ExecuteJavaScript("window.scrollTo(0, document.body.scrollHeight)");
        Thread.Sleep(1000);

        Actions actions = new Actions(webDriver);

        while (ViewMoreExist())
        {
            actions.MoveToElement(webDriver.FindElement(By.CssSelector("a.search-results__view-more"))).Click().Perform();
            Thread.Sleep(1000);
        }
    }
    private bool ViewMoreExist()
    {
        //element is always present in the page source, but got hidden when no longer needed
        var element = webDriver.FindElement(By.CssSelector("a.search-results__view-more"));
        return element.Enabled && element.Displayed;
    }

    private void ValidateLinks(string searchedText)
    {
        var links = webDriver.FindElements(By.XPath("//h3[@class='search-results__title']/a"));

        //Validate that all links in a list contain the word “BLOCKCHAIN”/”Cloud”/”Automation” in the text. LINQ should be used. 
        var linksWithText =
            from link in links
            where link.Text.ToLower().Contains(searchedText.ToLower())
            select link;

        Assert.That(linksWithText.Count(), Is.EqualTo(links.Count));

    }
}