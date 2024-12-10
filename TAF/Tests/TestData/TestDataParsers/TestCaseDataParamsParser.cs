using Newtonsoft.Json;
using TAF.Tests.TestData;
using TAF.Tests.TestData.TestDataModels;

namespace TAF.Tests.TestData.TestDataParsers
{
  public class TestCaseDataParamsParser
  {
    public static IEnumerable<TestCaseData> GetTestDataFromJson(string path)
    {
      var json = File.ReadAllText(path);
      var testData = JsonConvert.DeserializeObject<List<TestInvalidParams>>(json);

      foreach (var data in testData)
      {
        yield return new TestCaseData(data.Description, data.Name);
      }
    }

    public static IEnumerable<TestCaseData> PostWithWrongParams()
    {
      return GetTestDataFromJson(TestDataPaths.PostWithWrongParams);
    }
  }
}
