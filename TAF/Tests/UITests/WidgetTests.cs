using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAF.Business.Constants;
using TAF.Business.PageObjects;
using TAF.Core.BaseClasses;
using TAF.Core.Utilities.Helpers;

namespace TAF.Tests.UITests
{
  public class WidgetTests : BaseUiTest
  {
    [Test]
    public void CheckAddWidget()
    {
      browser.GoToPage(DashboardPages.TestDashboard);

      var widgetName = "Widget " + DateTime.Now;

      var addWidgetPage = new AddWidgetPage();
      addWidgetPage.AddWidget(widgetName);

      var dashboardElement = WaitForElementToBeVisible(By.XPath($"//div[contains(@class, 'widgetHeader__widget-name-block--AOAHS') and text()='{widgetName}']"));

      Assert.That(dashboardElement, Is.Not.Null);
    }

    [Test]
    public void CheckScrollToWidget()
    {
      browser.GoToPage(DashboardPages.DemoDashboard);

      var widgetPage = new WidgetPage();
      widgetPage.ScrollToWidget();

      var isWidgetInView = widgetPage.IsWidgetInView();

      Assert.That(isWidgetInView, Is.True);
    }
  }
}
