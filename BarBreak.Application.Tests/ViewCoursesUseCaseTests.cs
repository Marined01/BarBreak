using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit; // для тестів
using Moq; // для створення mock-об’єктів
using BarBreak.Application.UseCases;
using BarBreak.Core.Repositories;
using BarBreak.Core.DTOs;
using BarBreak.Core.Entities;
using BarBreak.Core.Course;

namespace BarBreak.Application.Tests
{
    public class ViewCoursesUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_ValidUserId_ReturnsCourseDtos()
        {
            // Arrange
            var mockRepo = new Mock<ICourseRepository>();
            var courses = new List<CourseEntity>
            {
                new CourseEntity { Id = 1, /* Додайте інші властивості CourseEntity */ },
                new CourseEntity { Id = 2, /* Додайте інші властивості CourseEntity */ }
            };
            mockRepo.Setup(repo => repo.GetCoursesForUserAsync(1)).ReturnsAsync(courses);

            var useCase = new ViewCoursesUseCase(mockRepo.Object);

            // Act
            var result = await useCase.ExecuteAsync(1);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].Id); // Перевірка ID
            // Додайте додаткові перевірки для властивостей
        }

        [Fact]
        public async Task ExecuteAsync_InvalidUserId_ReturnsEmptyList()
        {
            // Arrange
            var mockRepo = new Mock<ICourseRepository>();
            var useCase = new ViewCoursesUseCase(mockRepo.Object);

            // Act
            var result = await useCase.ExecuteAsync(-1);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task ExecuteAsync_UserHasNoCourses_ReturnsEmptyList()
        {
            // Arrange
            var mockRepo = new Mock<ICourseRepository>();
            mockRepo.Setup(repo => repo.GetCoursesForUserAsync(1)).ReturnsAsync(new List<CourseEntity>());

            var useCase = new ViewCoursesUseCase(mockRepo.Object);

            // Act
            var result = await useCase.ExecuteAsync(1);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task ExecuteAsync_RepositoryThrowsException_ThrowsException()
        {
            // Arrange
            var mockRepo = new Mock<ICourseRepository>();
            mockRepo.Setup(repo => repo.GetCoursesForUserAsync(1)).ThrowsAsync(new Exception("Database error"));

            var useCase = new ViewCoursesUseCase(mockRepo.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync(1));
        }
    }
}


