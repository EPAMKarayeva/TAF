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
  public class UpdateDashboardTest : BaseUiTest
  {
    [Test]
    public void CheckUpdateBoard()
    {
      var newBoardName = "NEW BOARD " + DateTime.Now;

      var createDashboardPage = new CreateDashboardPage();
      createDashboardPage.CreateDashboard(newBoardName, "");
      createdDashboards.Add(newBoardName);

      var editPage = new EditDashboardPage();
      editPage.RenameDashboard(newBoardName);

      browser.GoToPage(DashboardPages.PersonalDashboard);

      var dashboardElement = WaitForElementToBeVisible(By.XPath($"//a[contains(text(), '{newBoardName}')]"));

      Assert.That(dashboardElement, Is.Not.Null);
    }
  }
}
