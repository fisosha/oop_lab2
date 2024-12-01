namespace Laba2.Models
{
    public class Lecture
    {
        public string Day { get; set; } 
        public string Time { get; set; } 
        public string Lecturer { get; set; } 
        public string Department { get; set; } 
        public string Room { get; set; } 
        public List<Student> Students { get; set; } = new List<Student>();
    }
}