using OnlineCourse.Domain._Base;
using OnlineCourse.Domain.Commons;
using OnlineCourse.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineCourse.Domain.Students
{
    public class StudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public void Add(StudentDto studentDto)
        {
            var studentAlreadySave = _studentRepository.GetByIdentificationId(studentDto.IdentificationId);

            RuleValidator.New()
                .When(studentAlreadySave != null, Messages.IDENTIFICATION_ID_IS_ALREADY_EXISTS)
                .When(!Enum.TryParse<TargetAudience>(studentDto.TargetAudience, out var targetAudience), Messages.INVALID_TARGETAUDIENCE)
                .ThrowExceptionIfExists();

            var student = new Student(studentDto.Name, studentDto.IdentificationId, studentDto.Email, targetAudience);
            
            _studentRepository.Add(student);

        }

        public IEnumerable<StudentListDto> GetAll()
        {
            return _studentRepository.GetAll().Select(s => new StudentListDto
            {
                Name = s.Name,
                IdentificationId = s.IdentificationId,
                Email = s.Email,
                TargetAudience = s.TargetAudience.ToString()
            });
        }

        public void Update(StudentDto studentDto)
        {
            var studentAlreadySave = _studentRepository.GetById(studentDto.Id);

            if (studentAlreadySave == null)
            {
                return;
            }

            studentAlreadySave.ChangeName(studentDto.Name);

            _studentRepository.Update(studentAlreadySave);
        }
    }
}
