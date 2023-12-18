using BuildYourHead.Api.Controllers.Recipe.Requests;
using BuildYourHead.Application.Services;
using Moq;

namespace BuildYourHead.Tests.Controllers.Recipe.Requests
{
    public class GetRecipesRequestHandlerTests
    {
        [Fact]
        public void Handle_NoArgs_CallsRecipeServiceGetAll()
        {
            // Arrange
            var recipeServiceMock = new Mock<IRecipeService>();
            var handler = new GetRecipesRequestHandler(recipeServiceMock.Object);

            // Act
            handler.Handle();

            // Assert
            recipeServiceMock.Verify(s => s.GetAll());
        }
    }
}
