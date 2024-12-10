using Newtonsoft.Json;
using TAF.Tests.TestData;
using TAF.Tests.TestData.TestDataModels;

namespace TAF.Tests.TestData.TestDataParsers
{
  public static class TestCaseDataParser
  {
    public static IEnumerable<TestCaseData> GetTestDataFromJson(string path)//мы не посмотрели апишные тесты,
                                                                            //так что пока не меняю этот класс
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
      return GetTestDataFromJson(TestDataPaths.GetWithWrongApi);
    }

    public static IEnumerable<TestCaseData> GetWrongId()
    {
      return GetTestDataFromJson(TestDataPaths.GetWithWrongId);
    }

    public static IEnumerable<TestCaseData> GetAnotherId()
    {
      return GetTestDataFromJson(TestDataPaths.GetWithAnotherId);
    }
  }
}