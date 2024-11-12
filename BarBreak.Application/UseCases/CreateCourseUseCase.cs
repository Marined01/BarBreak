using BarBreak.Core.Course;
using System;

namespace BarBreak.Application.UseCases
{
    public class CreateCourseUseCase
    {
        private readonly ICourseRepository _repository;

        public string CourseName { get; private set; }
        public string Description { get; private set; }
        public string Content { get; private set; }

        public CreateCourseUseCase(ICourseRepository repository)
        {
            _repository = repository;
        }

        public void Execute(string courseName, string description, string content)
        {
            if (string.IsNullOrWhiteSpace(courseName))
                throw new ArgumentException("Course name cannot be empty.");
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be empty.");
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Content cannot be empty.");

            CourseName = courseName;
            Description = description;
            Content = content;

            _repository.Save(courseName, description, content);
        }
    }
}