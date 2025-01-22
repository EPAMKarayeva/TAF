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
      try
      {
        IWebElement visibleElement = wait.Until(d => element);
        visibleElement.Click();
      }
      catch (NoSuchElementException)
      {
        logger.Error("Element is not found on the page.");
        throw;
      }
      catch (ElementNotInteractableException)
      {
        logger.Error("Element is not interactable.");
        throw;
      }
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