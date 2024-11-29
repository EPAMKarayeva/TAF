using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace TAF.Business.Page_Objects
{
  public class DashboardPage(IWebDriver driver) : BasePage(driver)
  {
    private IWebElement Widget => WaitForVisbility(By.XPath("//div[contains(@class, 'widgetHeader__widget-name-block--AOAHS') and text()='LAUNCHES DURATION CHART']"));
    private IWebElement Source => FindElement(By.XPath("//div[contains(@class, 'widgetHeader__widget-name-block--AOAHS') and text()='LAUNCH STATISTICS AREA']"));
    private IWebElement Target => FindElement(By.XPath("//div[contains(@class, 'widgetHeader__widget-name-block--AOAHS') and text()='LAUNCH STATISTICS BAR']"));

    public void DragAndDrop()
    {
      Actions actions = new Actions(driver);
      actions.DragAndDrop(Source, Target).Perform();
    }

    public void ResizeElement( int xOffset, int yOffset, int desiredIndex)
    {
      Actions actions = new Actions(driver);
      var spanElements = driver.FindElements(By.XPath("//span[contains(@class, 'react-resizable-handle') and contains(@class, 'react-resizable-handle-se')]"));
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