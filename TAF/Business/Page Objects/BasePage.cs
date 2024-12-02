using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TAF.Business.Page_Objects
{
  public class BasePage
  {
    protected readonly IWebDriver driver;
    protected static WebDriverWait wait;
    protected static Logger logger = LogManager.GetCurrentClassLogger();

    public BasePage(IWebDriver driver)
    {
      this.driver = driver;
      wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
    }

    protected IWebElement FindElement(By selector)
    {
      return driver.FindElement(selector);
    }

    protected IWebElement WaitForVisbility(By locator)
    {
      return wait.Until(d =>
      {
        var element = d.FindElement(locator);
        return element;
      });
    }

    protected void ClickJS(IWebElement element)
    {
      IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
      js.ExecuteScript("arguments[0].click();", element);
    }
  }
}