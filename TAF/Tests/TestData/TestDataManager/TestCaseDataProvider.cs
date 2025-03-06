namespace TAF.Tests.TestData.TestDataManager
{
  public class TestCaseDataProvider
  {
    public static IEnumerable<object[]> GetTestDataFromJson(string path)
    {
      var json = File.ReadAllText(path);
      var testData = TestCaseDataParser.ConvertJson(json);

      return TestCaseSourceGenerator.GenerateCase(testData);
    }

  }
}
