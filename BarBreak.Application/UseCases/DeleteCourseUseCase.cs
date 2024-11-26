using BarBreak.Core.Course;
using System;

namespace BarBreak.Application.UseCases
{
    public interface ICourseRepository
    {
        void Delete(string courseName);
    }

    public class DeleteCourseUseCase
    {
        private readonly ICourseRepository _repository;

        public DeleteCourseUseCase(ICourseRepository repository)
        {
            _repository = repository;
        }

        public void Execute(string courseName)
        {
            if (string.IsNullOrWhiteSpace(courseName))
            {
                throw new ArgumentException("Course name cannot be empty.");
            }
            _repository.Delete(courseName);
        }
    }
}