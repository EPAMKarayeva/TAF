using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAF.Core.BaseClasses;

namespace TAF.Business.PageObjects
{
  internal class WidgetPage : BaseDriver
  {
    private IWebElement Widget => FindElement(By.XPath("//div[contains(@class, 'widgetHeader__widget-name-block') and contains(text(), 'LAUNCHES DURATION CHART')]"));
    private IWebElement OriginalWidgetLocation => FindElement(By.XPath("//div[contains(@class, 'widgetHeader__widget-name-block') and text()='LAUNCH STATISTICS AREA']"));
    private IWebElement TargetWidgetLocation => FindElement(By.XPath("//div[contains(@class, 'widgetHeader__widget-name-block') and text()='LAUNCH STATISTICS BAR']"));
    private IList<IWebElement> AllWidgets => FindElements(By.XPath("//span[contains(@class, 'react-resizable-handle') and contains(@class, 'react-resizable-handle-se')]"));

    public void DragAndDropWidget()
    {
      Actions actions = new Actions(driver);
      actions.DragAndDrop(OriginalWidgetLocation, TargetWidgetLocation).Perform();
    }

    public void ResizeWidget(int xOffset, int yOffset, int desiredIndex)
    {
      Actions actions = new Actions(driver);
      var specificSpanElement = AllWidgets[desiredIndex];
      actions.ClickAndHold(OriginalWidgetLocation)
          .MoveByOffset(xOffset, yOffset)
          .Release()
          .Build()
          .Perform();
    }

    public void ScrollToWidget()
    {
      ScrollToElement(Widget);
    }

    public bool IsWidgetInView()
    {
      return IsElementInView(Widget);
    }
  }
}
