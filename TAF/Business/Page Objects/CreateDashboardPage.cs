using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAF.Business.Page_Objects
{
  public class CreateDashboardPage(IWebDriver driver): BasePage(driver)
  {
    private IWebElement CreateButton => WaitForVisbility(By.XPath("//button[.//span[text()='Add New Dashboard']]"));
    private IWebElement InputName => FindElement(By.CssSelector("input[placeholder=\"Enter dashboard name\"]"));
    private IWebElement AddButton => FindElement(By.XPath("//button[@type='button' and text()='Add']"));

    public void PerformCreate(string name, string description)
    {
      ClickJS(CreateButton);
      InputName.SendKeys(name);
      logger.Info("Name entered");
      ClickJS(AddButton);
      logger.Info("Dashboard created");
    }
  }
}
