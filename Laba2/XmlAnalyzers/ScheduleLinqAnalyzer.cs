using System.Xml.Linq;
using Laba2.Interfaces;
using Laba2.Models;

namespace Laba2.XmlAnalyzers
{
    public class ScheduleLinqAnalyzer : IScheduleXmlAnalyzer
    {
        public Schedule Analyze(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);

            return new Schedule
            {
                Lectures = doc.Descendants("lecture")
                              .Select(ScheduleConverter.BuildLecture)
                              .ToList()
            };
        }

        public Schedule SearchByAttribute(string xmlFilePath, string searchValue)
        {
            var results = new List<string>();

            var doc = XDocument.Load(xmlFilePath);
            
            var schedule = new Schedule
            {
                Lectures = doc.Descendants("lecture")
                              .Select(ScheduleConverter.BuildLecture)
                              .ToList()
            };

            return new Schedule
            {
                Lectures = doc.Descendants("lecture")
                .Where(element => element.Attributes()
                    .Any(attr => attr.Value.Contains(searchValue, StringComparison.OrdinalIgnoreCase)))
                .Select(ScheduleConverter.BuildLecture)
                              .ToList()
            };
        }
    }
}
