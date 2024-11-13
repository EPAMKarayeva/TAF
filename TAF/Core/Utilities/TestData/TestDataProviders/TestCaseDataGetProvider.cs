using Newtonsoft.Json;
using TAF.Core.Utilities.Contants;
using TAF.Core.Utilities.TestData.TestDataHolders;

namespace TAF.Core.Utilities.TestData.TestDataProviders
{
  public static class TestCaseDataGetProvider
  {
    public static IEnumerable<TestCaseData> GetTestDataFromJson(string path)
    {
      var json = File.ReadAllText(path);
      var testData = JsonConvert.DeserializeObject<List<TestInvalidParameter>>(json);

      foreach (var data in testData)
      {
        yield return new TestCaseData(data.InvalidParameter);
      }
    }

    public static IEnumerable<TestCaseData> GetWrongApi()
    {
      return GetTestDataFromJson(FilePaths.GetWithWrongApi);
    }

    public static IEnumerable<TestCaseData> GetWrongId()
    {
      return GetTestDataFromJson(FilePaths.GetWithWrongId);
    }

    public static IEnumerable<TestCaseData> GetAnotherId()
    {
      return GetTestDataFromJson(FilePaths.GetWithAnotherId);
    }
  }
}