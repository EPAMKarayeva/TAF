using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAF.Business.Page_Objects
{
  public class DeleteDashboardPage(IWebDriver driver) : BasePage(driver)
  {
    private IWebElement DeleteButton => WaitForVisbility(By.XPath("//button[.//span[text()='Delete']]"));
    private IWebElement SubmitButton => FindElement(By.XPath("//button[@type='button' and text()='Delete']"));

    public void PerformDelete()
    {
      ClickJS(DeleteButton);
      ClickJS(SubmitButton);
      logger.Info("Dashboard deleted");
    }
  }
}
