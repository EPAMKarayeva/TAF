using OpenQA.Selenium;
using TAF.Core.BaseClasses;
using TAF.Core.Utilities.Helpers;

namespace TAF.Business.PageObjects
{
  public class DeleteDashboardPage() : BaseDriver()
  {
    private IWebElement DeleteButton => FindElement(By.XPath("//button[.//span[text()='Delete']]"));
    private IWebElement ConfirmDeleteButton => FindElement(By.XPath("//button[@type='button' and text()='Delete']"));

    public void DeleteDashboard()
    {
      DeleteButton.ClickWithWait();
      ConfirmDeleteButton.ClickWithWait();
      logger.Info("Dashboard deleted");
    }
  }
}
