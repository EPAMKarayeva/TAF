using OpenQA.Selenium;
using TAF.Core.BaseClasses;
using TAF.Core.Utilities.Helpers;

namespace TAF.Business.PageObjects
{
  public class AddWidgetPage(IWebDriver driver) : BaseDriver(driver)
  {
    private IWebElement AddWidgetButton => FindElement(By.XPath("//button[.//span[text()='Add new widget']]"));
    private IWebElement PickWidget => FindElement(By.XPath("//span[contains(@class, 'inputRadio__toggler') and contains(@class, 'inputRadio__toggler-medium')]"));
    private IWebElement NextButton => FindElement(By.XPath("//button[.//span[text()='Next step']]"));
    private IWebElement PickFilter => FindElement(By.XPath("//span[contains(@class, 'inputRadio__toggler') and contains(@class, 'inputRadio__at-top') and contains(@class, 'inputRadio__mode-default') and contains(@class, 'inputRadio__toggler-medium')]"));
    private IWebElement InputName => FindElement(By.CssSelector("input[placeholder=\"Enter widget name\"]"));
    private IWebElement AddButton => FindElement(By.XPath("//button[@type='button' and text()='Add']"));

    public void AddWidget(string name)
    {
      AddWidgetButton.Click();
      WaitForVisbility(PickWidget).Click();
      WaitForVisbility(NextButton).Click();
      logger.Info("Widget is picked");
      WaitForVisbility(PickFilter).Click();
      WaitForVisbility(NextButton).Click();
      logger.Info("Filter is picked");
      InputHelper.InputText(InputName, name);
      WaitForVisbility(AddButton).Click();
      logger.Info($"Widget with name '{name}' is added");
    }
  }
}
