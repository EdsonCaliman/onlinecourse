namespace OnlineCourse.Domain.Registrations
{
    public class RegistrationDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public decimal Value { get; set; }
    }
}
