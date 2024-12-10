using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TAF.Core.Utilities.Helpers
{
  public static class InputHelper
  {
    public static void InputText(IWebElement element, string text)
    {
      element.Click();
      element.SendKeys(Keys.Control + "a");
      element.SendKeys(Keys.Backspace);
      element.SendKeys(text);
    }
  }
}
