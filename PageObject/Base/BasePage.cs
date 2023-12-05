using OpenQA.Selenium;

public class BasePage{
     protected IWebDriver webDriver;

     public BasePage(IWebDriver webDriver){
        this.webDriver = webDriver;
    } 

}