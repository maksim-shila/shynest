using BuildYourHead.Api.Controllers.Recipe.Requests;
using BuildYourHead.Api.Exceptions;
using BuildYourHead.Application.Services;
using Moq;

namespace BuildYourHead.Tests.Controllers.Recipe.Requests
{
    public class DeleteRecipeRequestHandlerTests
    {
        [Fact]
        public void Handle_ValidId_CallsRecipeServiceDelete()
        {
            // Arrange
            var recipeServiceMock = new Mock<IRecipeService>();
            recipeServiceMock.Setup(s => s.Delete(It.IsAny<int>()));
            var handler = new DeleteRecipeRequestHandler(recipeServiceMock.Object);

            // Act
            const int id = 2;
            handler.Handle(id);

            // Assert
            recipeServiceMock.Verify(s => s.Delete(id));
        }

        [Fact]
        public void Handle_NegativeId_ThrowsValidationException()
        {
            // Arrange
            var recipeServiceMock = new Mock<IRecipeService>();
            var handler = new DeleteRecipeRequestHandler(recipeServiceMock.Object);

            // Act, Assert
            const int id = -1;
            Assert.Throws<ValidationException>(() => handler.Handle(id));
        }

        [Fact]
        public void Handle_ZeroId_ThrowsValidationException()
        {
            // Arrange
            var recipeServiceMock = new Mock<IRecipeService>();
            var handler = new DeleteRecipeRequestHandler(recipeServiceMock.Object);

            // Act, Assert
            const int id = 0;
            Assert.Throws<ValidationException>(() => handler.Handle(id));
        }
    }
}
