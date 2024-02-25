using Xunit;
using DemoAPI.Controllers;
using DemoAPI.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;

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
    }

}
