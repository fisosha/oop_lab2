using Laba2.Interfaces;
using Laba2.Models;

namespace Laba2
{
    public class SchedulerHtmlGenerator
    {
        private readonly IScheduleXmlAnalyzer _xmlAnalyzer;

        public SchedulerHtmlGenerator(IScheduleXmlAnalyzer xmlAnalyzer)
        {
            _xmlAnalyzer = xmlAnalyzer ?? throw new ArgumentNullException(nameof(xmlAnalyzer));
        }

        public string GenerateHtmlSchedule(string xmlFile)
        {
            try
            {
                var schedule = _xmlAnalyzer.Analyze(xmlFile);

                var htmlContent = GenerateHtml(schedule);

                var filePath = Path.Combine("C:\\Users\\shala\\OneDrive\\schedule.html");

                File.WriteAllText(filePath, htmlContent);

                return filePath;
            }
            catch (Exception ex)
            {
                throw new Exception($"Помилка при генерації HTML: {ex.Message}");
            }
        }

        public string GenerateHtmlSchedule(string xmlFile, string searchValue)
        {
            try
            {
                var schedule = _xmlAnalyzer.SearchByAttribute(xmlFile, searchValue);

                var htmlContent = GenerateHtml(schedule);

                var filePath = Path.Combine("C:\\Users\\shala\\OneDrive\\schedule.html");

                File.WriteAllText(filePath, htmlContent);

                return filePath;
            }
            catch (Exception ex)
            {
                throw new Exception($"Помилка при генерації HTML: {ex.Message}");
            }
        }


        private string GenerateHtml(Schedule schedule)
        {
            var html = "<html><body><h1>Розклад лекцій</h1><table border='1' cellpadding='5' cellspacing='0' style='border-collapse: collapse;'>";

            html += "<tr><th>День</th><th>Час</th><th>Викладач</th><th>Кафедра</th><th>Аудиторія</th><th>Студенти</th></tr>";

            foreach (var lecture in schedule.Lectures)
            {
                html += "<tr>";

                html += $"<td>{lecture.Day}</td>";
                html += $"<td>{lecture.Time}</td>";
                html += $"<td>{lecture.Lecturer}</td>";
                html += $"<td>{lecture.Department}</td>";
                html += $"<td>{lecture.Room}</td>";

                html += "<td><ul>";
                foreach (var student in lecture.Students)
                {
                    html += $"<li>{student.Name} ({student.Group})</li>";
                }
                html += "</ul></td>";

                html += "</tr>";
            }

            html += "</table></body></html>";
            return html;
        }
    }
}
