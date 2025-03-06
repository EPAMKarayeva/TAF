using Newtonsoft.Json.Linq;

namespace TAF.Tests.TestData.TestDataManager
{
  public static class TestCaseDataParser
  {
    public static JArray ConvertJson(string testData)
    {
      return JArray.Parse(testData);
    }
  }
}