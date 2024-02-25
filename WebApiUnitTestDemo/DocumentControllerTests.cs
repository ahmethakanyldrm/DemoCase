using Xunit;
using DemoAPI.Controllers;
using DemoAPI.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WebApiUnitTestDemo
{
    public class DocumentControllerTests
    {
        [Fact]
        public async Task CreateDocument_ValidDocument_ReturnsOkResult()
        {
            // Arrange
            var dbContext = GetDbContext();
            var controller = new DocumentController(dbContext);
            var document = new Document { id = 1, Name = "Test Document", Path = "Lorem Ipsum" };

            // Act
            var result = await controller.CreateDocument(document);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task CreateDocument_InvalidDocument_ReturnsBadRequestResult()
        {
            // Arrange
            var dbContext = GetDbContext();
            var controller = new DocumentController(dbContext);
            // Create an invalid document (e.g., missing required fields)
            var document = new Document { id = 1, Name = null, Path = "Invalid Content" };

            // Act
            var result = await controller.CreateDocument(document);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task CreateDocument_DbError_ReturnsNotFoundResult()
        {
            // Arrange
            var dbContext = GetFaultyDbContext();
            var controller = new DocumentController(dbContext);
            var document = new Document { id = 1, Name = "Test Document", Path = "Lorem Ipsum" };

            // Act
            var result = await controller.CreateDocument(document);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteDocument_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var dbContext = GetDbContextWithData();
            var controller = new DocumentController(dbContext);
            var documentId = 1;

            // Act
            var result = await controller.DeleteDocument(documentId);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteDocument_InvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var dbContext = GetFaultyDbContext(); // Use a faulty context that always returns null
            var controller = new DocumentController(dbContext);
            // Use an invalid document id (non-existing id)
            var documentId = 999;

            // Act
            var result = await controller.DeleteDocument(documentId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteDocument_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var dbContext = GetDbContext();
            var controller = new DocumentController(dbContext);
            var documentId = 1;

            // Act
            var result = await controller.DeleteDocument(documentId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        private DemoDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<DemoDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new DemoDbContext(options);
        }

        private DemoDbContext GetDbContextWithData()
        {
            var dbContext = GetDbContext();

            dbContext.Documents.AddRange(new Document
            {
                id = 1,
                Name = "Test Document 1",
                Path = "Lorem Ipsum 1"
            });

            dbContext.SaveChanges();

            return dbContext;
        }

        private DemoDbContext GetFaultyDbContext()
        {
            // Simulate a faulty DbContext for error scenarios
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<DemoDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            return new DemoDbContext(options);
        }
    }

}
