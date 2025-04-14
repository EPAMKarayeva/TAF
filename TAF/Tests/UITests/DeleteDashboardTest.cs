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
  public class DeleteDashboardTest : BaseUiTest
  {
    [Test]
    public void CheckDeleteBoard()
    {
      var dashboardName = "BOARD FOR DELETION";

      var createDashboardPage = new CreateDashboardPage();
      createDashboardPage.CreateDashboard(dashboardName, "");

      browser.RefreshPage();
      browser.GoToPage(DashboardPages.PersonalDashboard);

      var deletePage = new DeleteDashboardPage(dashboardName);
      deletePage.DeleteDashboardByName();

      browser.RefreshPage();

      var dashboardElements = WaitForElementsToBeVisible(By.XPath($"//a[contains(text(), '{dashboardName}')]"));

      Assert.That(dashboardElements, Is.Empty);
    }
  }
}