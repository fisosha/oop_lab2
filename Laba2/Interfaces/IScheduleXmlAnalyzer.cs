using Laba2.Models;

namespace Laba2.Interfaces
{
    public interface IScheduleXmlAnalyzer
    {
        Schedule Analyze(string filePath);

        Schedule SearchByAttribute(string xmlFilePath, string searchValue);
    }
}
