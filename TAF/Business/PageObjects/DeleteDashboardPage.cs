using OpenQA.Selenium;
using TAF.Core.BaseClasses;

namespace TAF.Business.PageObjects
{
  public class DeleteDashboardPage(IWebDriver driver) : BaseDriver(driver)
  {
    private IWebElement DeleteButton => FindElement(By.XPath("//button[.//span[text()='Delete']]"));
    private IWebElement SubmitButton => FindElement(By.XPath("//button[@type='button' and text()='Delete']"));

    public void Delete()
    {
      WaitForVisbility(DeleteButton).Click();
      SubmitButton.Click();
      logger.Info("Dashboard deleted");
    }
  }
}
