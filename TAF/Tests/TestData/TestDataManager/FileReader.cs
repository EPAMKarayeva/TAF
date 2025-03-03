using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAF.Tests.TestData.TestDataManager
{
  public static class FileReader
  {
    public static string ReadFile(string fileName)
    {
      return File.ReadAllText(fileName);
    }
  }
}
