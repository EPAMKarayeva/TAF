using Newtonsoft.Json.Linq;

namespace TAF.Tests.TestData.TestDataManager
{
  public static class TestCaseDataParser
  {
    public static JObject ConvertJson(string testData)
    {
      return JObject.Parse(testData);
    }
  }
}