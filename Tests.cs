using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework.Internal;

namespace TA_Task1;

public class Tests
{
    private IWebDriver webDriver;
    

    [SetUp]
    public void Setup()
    {
        webDriver = new ChromeDriver();
    }

    // [TearDown]
    // public void CleanUp()
    // {
    //     webDriver.Quit();
    // }

    [Test]
    [TestCase("C#", "All Locations")]
    public void Test1ValidateThatTheUserCanSearchForPositionBasedOnCriteria(string skill, string location)
    {
        HomePage homepage = new HomePage(webDriver);
        CareersPage careerspage;
        IWebElement text;

        careerspage = homepage.Open()
                .AcceptAllCookies() 
                .ClickOnCareersLink();

        text = careerspage.InputToField(skill)
                .ChooseFromDropDownList(location)
                .CheckRemoteCheckBox()
                .ClickOnFindButton()
                .OpenTheLastOffer()
                .GetText(skill);

        Assert.IsTrue(text.Text.Contains(skill));
    }

    [Test]
    [TestCase("BLOCKCHAIN")]
    [TestCase("Cloud")]
    [TestCase("Automation")]
    public void ValidateIfGlobalSearchWorksAsExpected(string searchedText)
    {
        HomePage homepage = new HomePage(webDriver);
        SearchResultPage searchresultpage;
    
        searchresultpage = homepage.Open()
            .AcceptAllCookies() 
            .ClickOnMagnifier()
            .SearchText(searchedText)
            .ClickOnFindButton2();
        
        searchresultpage.LoadAllResults();

        var links = searchresultpage.ValidateLinks();
        
        //Validate that all links in a list contain the word “BLOCKCHAIN”/”Cloud”/”Automation” in the text. LINQ should be used. 
        var linksWithText =
            from link in links
            where link.Text.ToLower().Contains(searchedText.ToLower())
            select link;

        Assert.That(linksWithText.Count(), Is.EqualTo(links.Count));
    }

    [Test]
    [TestCase("EPAM_Corporate_Overview_Q3_october.pdf")]
    public void ValidateFileDownloadFunction(string downloadedFile)
    {
        HomePage homepage = new HomePage(webDriver);
        AboutPage aboutpage;
        
        aboutpage = homepage.Open()
                            .AcceptAllCookies()
                            .ClickOnAboutLink();
        Thread.Sleep(2000);
        aboutpage.ScrollToEpamAtaGlance()
                 .ClickOnDownloadButton();
        Thread.Sleep(5000); 
        Assert.IsTrue(File.Exists(@"/Users/karolina_kukula-biczewska/Downloads/"+downloadedFile));
    } 

    [Test]
    public void ValidateTitleOfTheArticleMatchesWithTitleInTheCarousel(){
        HomePage homepage = new HomePage(webDriver);
        InsightsPage insightspage;

        insightspage = homepage.Open()
                .AcceptAllCookies()
                .ClickOnInsightsLink();
        
        String slideText = insightspage.SwipePesentation()
                .SwipePesentation()
                .GetTheActiveSlideText();

        String path = insightspage.GetPath();
        String artcleTitle = insightspage.GoToUrl(path)
                .GetArticleTitle();

        Assert.That(artcleTitle.Contains(slideText));
    }
}