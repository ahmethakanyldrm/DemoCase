using Xunit;
using DemoAPI.Controllers;
using DemoAPI.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;


namespace WebApiUnitTestDemo
{

    public class LicenceControllerTests
    {
        private readonly Mock<DemoDbContext> _mockContext;
        private readonly Mock<License> _mockLicense;

        public LicenceControllerTests()
        {
            _mockContext = new Mock<DemoDbContext>();
            _mockLicense = new Mock<License>();
        }

        [Fact]
        public async Task GetLicenses_ReturnsOkResultWithListOfLicenses()
        {
            // Arrange
            var dbContext = GetDbContextWithData();
            var controller = new LicenseController(dbContext);

            // Act
            var result = await controller.GetLicenses();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var licenses = Assert.IsType<List<License>>(okResult.Value);
            Assert.Equal(3, licenses.Count); // Adjust the expected count based on your data
        }

        [Fact]
        public async Task GetLicenses_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var controller = new LicenseController(GetFaultyDbContext()); // Use a faulty DbContext

            // Act
            var result = await controller.GetLicenses(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateLicense_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var dbContext = GetDbContextWithData();
            var controller = new LicenseController(dbContext);
            var newLicense = new License { LicenseKey = "newkey", ExpirationDate = "27-02" };

            // Act
            var result = await controller.CreateLisence(newLicense);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var createdLicense = Assert.IsType<License>(createdAtActionResult.Value);
            Assert.Equal(newLicense.LicenseKey, createdLicense.LicenseKey);
        }

        [Fact]
        public async Task CreateLisence_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var controller = new LicenseController(GetDbContextWithData()); // Use a regular DbContext

            // Act
            controller.ModelState.AddModelError("LicenseKey", "LicenseKey is required"); // Add ModelState error
            var result = await controller.CreateLisence(new License { LicenseKey = null, ExpirationDate = "28-02" });

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateLisence_DbError_ReturnsNotFound()
        {
            // Arrange
            var controller = new LicenseController(GetFaultyDbContext()); // Use a faulty DbContext

            // Act
            var result = await controller.CreateLisence(new License { LicenseKey = "abc", ExpirationDate = "28-02" });

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateLicense_ReturnsNoContentResult()
        {
            // Arrange
            var dbContext = GetDbContextWithData();
            var controller = new LicenseController(dbContext);
            var updatedLicense = new License { id = 1, LicenseKey = "updatedkey", ExpirationDate = "28-02" };

            // Act
            var result = await controller.UpdateLicense(1, updatedLicense);

            // Assert
            Assert.IsType<NoContentResult>(result);
            var entity = await dbContext.Licenses.FirstOrDefaultAsync(i => i.id == 1);
            Assert.Equal(updatedLicense.LicenseKey, entity.LicenseKey);
        }

        [Fact]
        public async Task UpdateLicense_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var controller = new LicenseController(GetFaultyDbContext()); // Use a faulty DbContext

            // Act
            var result = await controller.UpdateLicense(1, new License { id = 2, LicenseKey = "new-key", ExpirationDate = "28-02" });

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public async Task DeleteLicense_ReturnsOkResult()
        {
            // Arrange
            var dbContext = GetDbContextWithData();
            var controller = new LicenseController(dbContext);

            // Act
            var result = await controller.DeleteLicense(1);

            // Assert
            Assert.IsType<OkResult>(result);
            var entity = await dbContext.Licenses.FirstOrDefaultAsync(i => i.id == 1);
            Assert.Null(entity); // Ensure the entity is deleted
        }

        [Fact]
        public async Task DeleteLicense_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var controller = new LicenseController(GetFaultyDbContext()); // Use a faulty DbContext

            // Act
            var result = await controller.DeleteLicense(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }


        private DemoDbContext GetDbContextWithData()
        {
            var options = new DbContextOptionsBuilder<DemoDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new DemoDbContext(options);

            dbContext.Licenses.AddRange(new List<License>
            {
                new License { id = 1, LicenseKey = "abc", ExpirationDate = "24-02" },
                new License { id = 2, LicenseKey = "def", ExpirationDate = "25-02" },
                new License { id = 3, LicenseKey = "ghj", ExpirationDate = "26-02" },
            });

            dbContext.SaveChanges();

            return dbContext;
        }

        public DemoDbContext GetFaultyDbContext()
        {
            // Creating a faulty DbContext by not using InMemoryDatabase
            var options = new DbContextOptionsBuilder<DemoDbContext>()
                .UseSqlServer("InvalidConnection") // Use an invalid connection string
                .Options;

            return new DemoDbContext(options);
        }


    }
}