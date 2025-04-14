using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAF.Business.Constants;
using TAF.Core.BaseClasses;

namespace TAF.Core.Utilities.Helpers
{
  public class Browser
  {
    private IWebDriver driver = DriverManager.GetDriver();

    public void GoToPage(string page)
    {
      driver.Navigate().GoToUrl(page);
    }

    public void RefreshPage()
    {
      driver.Navigate().Refresh();
    }
  }
}
