using System.Xml;
using Laba2.Interfaces;
using Laba2.Models;

namespace Laba2.XmlAnalyzers
{
    public class ScheduleDomAnalyzer : IScheduleXmlAnalyzer
    {
        public Schedule Analyze(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            Schedule schedule = new Schedule();
            foreach (XmlNode lectureNode in doc.SelectNodes("//lecture"))
            {
                Lecture lecture = ScheduleConverter.BuildLecture(lectureNode);

                foreach (XmlNode studentNode in lectureNode.SelectNodes("students/student"))
                {
                    lecture.Students.Add(ScheduleConverter.BuildStudent(studentNode));
                }

                schedule.Lectures.Add(lecture);
            }

            return schedule;
        }

        public Schedule SearchByAttribute(string xmlFilePath, string searchValue)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFilePath);

            Schedule filteredSchedule = new Schedule();
            foreach (XmlNode lectureNode in doc.SelectNodes("//lecture"))
            {
                bool lectureMatches = false;

                foreach (XmlAttribute attribute in lectureNode.Attributes)
                {
                    if (attribute.Value.Contains(searchValue, StringComparison.OrdinalIgnoreCase))
                    {
                        lectureMatches = true;
                        break;
                    }
                }

                Lecture lecture = ScheduleConverter.BuildLecture(lectureNode);
                foreach (XmlNode studentNode in lectureNode.SelectNodes("students/student"))
                {
                    bool studentMatches = false;
                    foreach (XmlAttribute attribute in studentNode.Attributes)
                    {
                        if (attribute.Value.Contains(searchValue, StringComparison.OrdinalIgnoreCase))
                        {
                            studentMatches = true;
                            break;
                        }
                    }
                    if (studentMatches)
                    {
                        lecture.Students.Add(ScheduleConverter.BuildStudent(studentNode));
                    }
                }
                if (lectureMatches || lecture.Students.Count > 0)
                {
                    filteredSchedule.Lectures.Add(lecture);
                }
            }

            return filteredSchedule;
        }
    }
}
