namespace TAF.Tests.TestData.TestDataManager
{
  public class TestCaseDataProvider
  {
    public static IEnumerable<object[]> GetTestDataByKey(string path, string key)
    {
      var json = FileReader.ReadFile(path);

      var jsonObject = TestCaseDataParser.ConvertJson(json);

      return TestCaseSourceGenerator.GenerateCasesByKey(jsonObject, key);
    }
  }
}