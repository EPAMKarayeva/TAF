using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAF.Core.BaseClasses;

namespace TAF.Tests
{
  [SetUpFixture]
  public class GlobalSetupTeardown
  {
    [OneTimeTearDown]
    public void RunAfterAllTests()
    {
      var driver = DriverManager.GetDriver();

      driver.Quit();
      driver.Dispose();
    }
  }
}
