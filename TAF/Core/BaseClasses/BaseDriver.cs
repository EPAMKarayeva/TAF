using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TAF.Core.BaseClasses
{
  public class BaseDriver
  {
    protected readonly IWebDriver driver;
    protected static WebDriverWait wait;
    protected static Logger logger = LogManager.GetCurrentClassLogger();

    public BaseDriver()
    {
      driver = DriverManager.GetDriver();
      wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    protected IWebElement FindElement(By selector)
    {
      return driver.FindElement(selector);
    }

    protected IList<IWebElement> FindElements(By selector)
    {
      return driver.FindElements(selector);
    }

    protected void ClickJS(IWebElement element)
    {
      IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
      js.ExecuteScript("arguments[0].click();", element);
    }

    public void ScrollToElement(IWebElement element)
    {
      IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
      js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
    }

    public bool IsElementInView(IWebElement element)
    {
      IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
      return (bool)js.ExecuteScript(
          "var rect = arguments[0].getBoundingClientRect();" +
          "return " +
          "(rect.top >= 0 && rect.left >= 0 && rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) && " +
          "rect.right <= (window.innerWidth || document.documentElement.clientWidth));",
          element);
    }
  }
}