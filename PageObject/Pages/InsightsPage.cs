using OpenQA.Selenium;

public class InsightsPage : BasePage {

    public InsightsPage(IWebDriver webdriver)
        :base(webdriver){
    }

    public InsightsPage SwipePesentation(){
        webDriver.FindElement(By.CssSelector("button.slider__right-arrow")).Click();
        return this;
    }

    public String GetTheActiveSlideText(){
        IWebElement activeSlide = webDriver.FindElement(By.CssSelector(".owl-item.active"));
        String slideText = activeSlide.Text.ToLower();
        Console.WriteLine(slideText + "Test Kaaaarooooo");
        
        return slideText;
    }

    public InsightsPage ClickOnActiveSlide(){
        webDriver.FindElement(By.XPath("//*[@class=\"owl-item active\"]//a")).Click();
        return this;
    }

    public String GetPath(){
        String path = webDriver.FindElement(By.XPath("//*[@class=\"owl-item active\"]//a")).GetAttribute("href");
        return path;
    }

    public InsightsPage GoToUrl(String path){
        webDriver.Navigate().GoToUrl(path);
        return this;
    }

    public String GetArticleTitle(){
        String title = webDriver.FindElement(By.XPath("//div[@class=\"article__container\"]//b")).Text.ToLower();
        return title;
    }

}