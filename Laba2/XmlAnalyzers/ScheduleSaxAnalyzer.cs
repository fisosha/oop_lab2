using System.Xml;
using Laba2.Interfaces;
using Laba2.Models;

namespace Laba2.XmlAnalyzers
{
    public class ScheduleSaxAnalyzer : IScheduleXmlAnalyzer
    {
        public Schedule Analyze(string filePath)
        {
            Schedule schedule = new Schedule();
            Lecture currentLecture = null;

            using (XmlReader reader = XmlReader.Create(filePath))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "lecture":
                                currentLecture = ScheduleConverter.BuildLecture(reader);
                                schedule.Lectures.Add(currentLecture);
                                break;

                            case "student":
                                currentLecture?.Students.Add(ScheduleConverter.BuildStudent(reader));
                                break;
                        }
                    }
                }
            }

            return schedule;
        }

        public Schedule SearchByAttribute(string filePath, string searchValue)
        {
            Schedule filteredSchedule = new Schedule();
            Lecture currentLecture = null;

            using (XmlReader reader = XmlReader.Create(filePath))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "lecture":
                                bool lectureMatches = false;
                                for (int i = 0; i < reader.AttributeCount; i++)
                                {
                                    reader.MoveToAttribute(i);
                                    if (reader.Value.Contains(searchValue, StringComparison.OrdinalIgnoreCase))
                                    {
                                        lectureMatches = true;
                                    }
                                }
                                if (lectureMatches)
                                {
                                    currentLecture = ScheduleConverter.BuildLecture(reader);
                                    filteredSchedule.Lectures.Add(currentLecture);
                                }
                                else
                                {
                                    currentLecture = null;
                                }

                                reader.MoveToElement();
                                break;

                            case "student":
                                if (currentLecture != null)
                                {
                                    bool studentMatches = false;
                                    for (int i = 0; i < reader.AttributeCount; i++)
                                    {
                                        reader.MoveToAttribute(i);
                                        if (reader.Value.Contains(searchValue, StringComparison.OrdinalIgnoreCase))
                                        {
                                            studentMatches = true;
                                        }
                                    }

                                    if (studentMatches)
                                    {
                                        currentLecture.Students.Add(ScheduleConverter.BuildStudent(reader));
                                    }

                                    reader.MoveToElement();
                                }
                                break;
                        }
                    }
                }
            }

            return filteredSchedule;
        }

    }
}