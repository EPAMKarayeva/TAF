using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using NLog;
using TAF.Core.BaseClasses;

namespace TAF.Core.Utilities.Helpers
{
  public static class WebElementExtensions 
  {
    private static Logger logger = LogManager.GetCurrentClassLogger();
    private static IWebDriver driver = DriverManager.GetDriver();
    private static WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

    public static void ClickWithWait(this IWebElement element)
    {
      IWebElement visibleElement;

      if (element.Displayed)
      {
        visibleElement = wait.Until(d => element);
      }
      else
      {
        logger.Error("Element is not visible.");
        throw new NoSuchElementException("Element is not visible and clickable");
      }

      visibleElement.Click();
    }

    public static void TypeText(this IWebElement element, string text)
    {
      element.Click();
      element.SendKeys(Keys.Control + "a");
      element.SendKeys(Keys.Backspace);
      element.SendKeys(text);
    }
  }
}