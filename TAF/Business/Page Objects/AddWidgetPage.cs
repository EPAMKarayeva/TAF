using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAF.Business.Page_Objects
{
  public class AddWidgetPage(IWebDriver driver) : BasePage(driver)
  {
    private IWebElement AddWidgetButton => WaitForVisbility(By.XPath("//button[.//span[text()='Add new widget']]"));
    private IWebElement PickWidget => WaitForVisbility(By.XPath("//span[contains(@class, 'inputRadio__toggler--ygpdQ') and contains(@class, 'inputRadio__toggler-medium--iSkOd')]"));
    private IWebElement NextButton => WaitForVisbility(By.XPath("//button[.//span[text()='Next step']]"));
    private IWebElement PickFilter => WaitForVisbility(By.XPath("//label[.//span[contains(@class, 'filterName__name--B4z4P') and text()='Name']]//input[@type='radio']"));
    private IWebElement InputName => WaitForVisbility(By.CssSelector("input[placeholder=\"Enter widget name\"]"));
    private IWebElement AddButton => WaitForVisbility(By.XPath("//button[@type='button' and text()='Add']"));

    public void PerformAddWidget(string name)
    {
      ClickJS(AddWidgetButton);
      ClickJS(PickWidget);
      ClickJS(NextButton);
      logger.Info("Widget is picked");
      ClickJS(PickFilter);
      ClickJS(NextButton);
      logger.Info("Filter is picked");
      InputName.Click();  
      InputName.SendKeys(Keys.Control + "a");  
      InputName.SendKeys(Keys.Backspace);
      InputName.SendKeys(name);
      ClickJS(AddButton);
      logger.Info($"Widget with name '{name}' is added");
    }
  }
}
