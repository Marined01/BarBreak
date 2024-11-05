using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit; // для тестів
using Moq; // для створення mock-об’єктів
using BarBreak.Application.UseCases;
using BarBreak.Core.Repositories;
using BarBreak.Core.DTOs;
using BarBreak.Core.Entities;

namespace BarBreak.Application.Tests
{
    public class ViewCoursesUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_ValidUserId_ReturnsCourseDtos()
        {
            // Arrange
            var mockRepo = new Mock<ICourseRepository>();
            var courses = new List<Course>
            {
                new Course { Id = 1, Title = "Course 1", Description = "Description 1" },
                new Course { Id = 2, Title = "Course 2", Description = "Description 2" }
            };
            mockRepo.Setup(repo => repo.GetCoursesForUserAsync(1)).ReturnsAsync(courses);

            var useCase = new ViewCoursesUseCase(mockRepo.Object);

            // Act
            var result = await useCase.ExecuteAsync(1);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Course 1", result[0].Title);
            Assert.Equal("Course 2", result[1].Title);
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
            mockRepo.Setup(repo => repo.GetCoursesForUserAsync(1)).ReturnsAsync(new List<Course>());

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

