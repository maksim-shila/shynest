using BuildYourHead.Api.Controllers.Recipe.Requests;
using BuildYourHead.Api.Exceptions;
using BuildYourHead.Application.Dto;
using BuildYourHead.Application.Services;
using Moq;

namespace BuildYourHead.Tests.Controllers.Recipe.Requests
{
    public class AddRecipeRequestHandlerTests
    {
        [Fact]
        public void Handle_ValidData_CallsRecipeServiceAdd()
        {
            // Arrange
            var recipeServiceMock = new Mock<IRecipeService>();
            recipeServiceMock
                .Setup(s => s.Add(It.IsAny<RecipeDto>()))
                .Returns(It.IsAny<RecipeDto>());
            var handler = new PutRecipeRequestHandler(recipeServiceMock.Object);

            // Act
            var request = new AddRecipeRequest
            {
                Name = "test name",
                Description = "test description"
            };
            var result = handler.Handle(request);

            // Assert
            recipeServiceMock.Verify(s => s.Add(
                It.Is<RecipeDto>(recipeDto =>
                    recipeDto.Name == request.Name &&
                    recipeDto.Description == request.Description)
            ));
        }

        [Fact]
        public void Handle_EmptyName_ThrowsValidationException()
        {
            // Arrange
            var recipeServiceMock = new Mock<IRecipeService>();
            var handler = new PutRecipeRequestHandler(recipeServiceMock.Object);

            // Act, Assert
            var request = new AddRecipeRequest { Name = null };
            Assert.Throws<ValidationException>(() => handler.Handle(request));
        }
    }
}
