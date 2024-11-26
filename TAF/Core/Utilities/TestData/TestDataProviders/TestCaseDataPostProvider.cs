using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAF.Core.Utilities.Contants;
using TAF.Core.Utilities.TestData.TestDataHolders;

namespace TAF.Core.Utilities.TestData.TestDataProviders
{
  public class TestCaseDataPostProvider
  {
    public static IEnumerable<TestCaseData> GetTestDataFromJson(string path)
    {
      var json = File.ReadAllText(path);
      var testData = JsonConvert.DeserializeObject<List<TestInvalidPostParams>>(json);

      foreach (var data in testData)
      {
        yield return new TestCaseData(data.Description, data.Name);
      }
    }

    public static IEnumerable<TestCaseData> PostWithWrongParams()
    {
      return GetTestDataFromJson(FilePaths.PostWithWrongParams);
    }
  }
}
