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

    private IWebElement Widget => FindElement(By.XPath("//div[contains(@class, 'widgetHeader__widget-name-block--AOAHS') and text()='LAUNCHES DURATION CHART']"));
    private IWebElement Source => FindElement(By.XPath("//div[contains(@class, 'widgetHeader__widget-name-block--AOAHS') and text()='LAUNCH STATISTICS AREA']"));
    private IWebElement Target => FindElement(By.XPath("//div[contains(@class, 'widgetHeader__widget-name-block--AOAHS') and text()='LAUNCH STATISTICS BAR']"));
    private IList<IWebElement> spanElements => FindElements(By.XPath("//span[contains(@class, 'react-resizable-handle') and contains(@class, 'react-resizable-handle-se')]"));

    public BaseDriver(IWebDriver driver)
    {
      this.driver = driver;
      wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
    }

    protected IWebElement FindElement(By selector)
    {
      return driver.FindElement(selector);
    }

    protected IList<IWebElement> FindElements(By selector)
    {
      return driver.FindElements(selector);
    }

    protected void WaitPageForLoad()
    {
      driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
    }

    protected IWebElement WaitForVisbility(IWebElement element)
    {
      return wait.Until(d => element.Displayed ? element : throw new NoSuchElementException());
    }

    protected void ClickJS(IWebElement element)
    {
      IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
      js.ExecuteScript("arguments[0].click();", element);
    }

    public void DragAndDrop()
    {
      Actions actions = new Actions(driver);
      actions.DragAndDrop(Source, Target).Perform();
    }

    public void ResizeElement(int xOffset, int yOffset, int desiredIndex)
    {
      Actions actions = new Actions(driver);
      var specificSpanElement = spanElements[desiredIndex];
      actions.ClickAndHold(Source)
          .MoveByOffset(xOffset, yOffset)
          .Release()
          .Build()
          .Perform();
    }

    public void ScrollToElement()
    {
      IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
      js.ExecuteScript("arguments[0].scrollIntoView(true);", Widget);
    }

    public bool IsElementInView()
    {
      IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
      return (bool)js.ExecuteScript(
          "var rect = arguments[0].getBoundingClientRect();" +
          "return " +
          "(rect.top >= 0 && rect.left >= 0 && rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) && " +
          "rect.right <= (window.innerWidth || document.documentElement.clientWidth));",
          Widget);
    }
  }
}