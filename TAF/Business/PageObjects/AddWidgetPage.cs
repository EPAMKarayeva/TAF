using OpenQA.Selenium;
using TAF.Core.BaseClasses;
using TAF.Core.Utilities.Helpers;

namespace TAF.Business.PageObjects
{
  public class AddWidgetPage : BaseDriver
  {
    private IWebElement AddWidgetButton => FindElement(By.XPath("//button[.//span[text()='Add new widget']]"));
    private IWebElement PickWidget => FindElement(By.XPath("//div[text()='Launch statistics chart']"));
    private IWebElement NextButton => FindElement(By.XPath("//button[.//span[text()='Next step']]"));
    private IWebElement PickDefaultFilter => FindElement(By.XPath("//span[contains(@class, 'inputRadio__toggler')]"));
    private IWebElement PickNameFilter => FindElement(By.XPath("//span[@class='filterName__name--B4z4P' and text()='Name']"));
    private IWebElement WidgetName => FindElement(By.CssSelector("input[placeholder=\"Enter widget name\"]"));
    private IWebElement AddButton => FindElement(By.XPath("//button[@type='button' and text()='Add']"));

    public void AddWidget(string name)
    {
      AddWidgetButton.ClickWithWait();
      PickWidget.ClickWithWait();
      NextButton.ClickWithWait();
      logger.Info("Widget is picked");
      PickFilter();
      NextButton.ClickWithWait();
      logger.Info("Filter is picked");
      WidgetName.TypeText(name);
      AddButton.ClickWithWait();
      logger.Info($"Widget with name '{name}' is added");
    }

    public void PickFilter()
    {
      try
      {
        PickNameFilter.ClickWithWait();
      }
      catch (NoSuchElementException)
      {
        try
        {
          PickDefaultFilter.ClickWithWait();
        }
        catch (NoSuchElementException)
        {
          logger.Info("Neither PickDemoFilter nor PickDefaultFilter could be found.");
        }
      }
    }
  }
}