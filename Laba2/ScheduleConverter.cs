using Laba2.Models;
using System.Xml;
using System.Xml.Linq;

namespace Laba2
{
    public static class ScheduleConverter
    {
        public static Lecture BuildLecture(XElement lecture)
        {
            if (lecture == null)
            {
                return new Lecture();
            }

            return new Lecture
            {
                Day = lecture.Attribute("day")?.Value,
                Time = lecture.Attribute("time")?.Value,
                Lecturer = lecture.Attribute("lecturer")?.Value,
                Department = lecture.Attribute("department")?.Value,
                Room = lecture.Attribute("room")?.Value,
                Students = lecture.Descendants("student").Select(BuildStudent).ToList()
            };
        }

        public static Student BuildStudent(XElement student)
        {
            if (student == null)
            {
                return new Student();
            }

            return new Student
            {
                Name = student.Attribute("name")?.Value,
                Group = student.Attribute("group")?.Value
            };
        }

        public static Lecture BuildLecture(XmlNode lectureNode)
        {
            if(lectureNode == null)
            {
                return new Lecture();
            }

            return new Lecture
            {
                Day = lectureNode.Attributes["day"]?.Value,
                Time = lectureNode.Attributes["time"]?.Value,
                Lecturer = lectureNode.Attributes["lecturer"]?.Value,
                Department = lectureNode.Attributes["department"]?.Value,
                Room = lectureNode.Attributes["room"]?.Value
            };
        }

        internal static Student BuildStudent(XmlNode studentNode)
        {
            if (studentNode == null)
            {
                return new Student();
            }

            return new Student
            {
                Name = studentNode.Attributes["name"]?.Value,
                Group = studentNode.Attributes["group"]?.Value
            };
        }

        public static Lecture? BuildLecture(XmlReader reader)
        {
            if (reader == null)
            {
                return new Lecture();
            }

            return new Lecture
            {
                Day = reader.GetAttribute("day"),
                Time = reader.GetAttribute("time"),
                Lecturer = reader.GetAttribute("lecturer"),
                Department = reader.GetAttribute("department"),
                Room = reader.GetAttribute("room"),
            };
        }

        public static Student BuildStudent(XmlReader reader)
        {
            if (reader == null)
            {
                return new Student();
            }

            return new Student
            {
                Name = reader.GetAttribute("name"),
                Group = reader.GetAttribute("group")
            };
        }
    }
}
