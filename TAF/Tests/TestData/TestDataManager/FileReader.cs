using NLog;

namespace TAF.Tests.TestData.TestDataManager
{
  public static class FileReader
  {
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    public static string ReadFile(string fileName)
    {
      try
      {
        var data = File.ReadAllText(fileName);
        logger.Info($"File read successfully: {fileName}");
        return data;
      }
      catch (FileNotFoundException ex)
      {
        logger.Error(ex, $"File not found: {fileName}");
        throw;
      }
      catch (Exception ex)
      {
        logger.Error(ex, $"Error reading file: {fileName}");
        throw;
      }
    }
  }
}