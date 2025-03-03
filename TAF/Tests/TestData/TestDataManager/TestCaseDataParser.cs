using Newtonsoft.Json.Linq;

namespace TAF.Tests.TestData.TestDataParsers
{
  public static class TestCaseDataParser
  {
    public static JArray ConvertJson(string testData)
    {
      return JArray.Parse(testData);
    }
  }
}