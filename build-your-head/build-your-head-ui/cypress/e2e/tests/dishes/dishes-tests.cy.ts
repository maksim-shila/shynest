import { RecipeApiMock } from "../../../core/mocks/recipe-api-mock";
import { Recipe } from "../../../core/models/recipe";
import { RecipePage } from "../../../core/sreen/recipe/recipe-page";
import { RecipesPage } from "../../../core/sreen/recipes/recipes-page";
import { Guid } from "../../../core/utils/guid";

describe("Recipe Create/Update/Delete", () => {

    const recipesPage = new RecipesPage();
    const recipesApi = new RecipeApiMock();

    beforeEach("Mock Recipes API", () => {
        recipesApi.setUp();
    });

    it("Add recipe -> new recipe created", () => {
        // Arrange
        const recipe: Recipe = {
            name: Guid.next(),
            description: "New Description"
        };

        // Act
        recipesPage
            .open()
            .clickAddRecipe()
            .fill(recipe)
            .clickAdd();

        // Assert
        new RecipePage(recipe).shouldBeOpened();
        recipesPage
            .open()
            .recipesList
            .shouldHaveRecipe(recipe);
    });

    it("Edit recipe -> recipe info changed", () => {
        // Arrange
        const recipeName = Guid.next();
        const recipe: Recipe = {
            name: recipeName,
            description: "New Description"
        };
        const updatedRecipe: Recipe = {
            name: recipeName + " Updated",
            description: "Updated Description"
        }
        recipesApi.addRecipe(recipe);

        // Act
        recipesPage
            .open()
            .recipesList
            .clickEdit(recipe)
            .shouldBePrepopulated(recipe)
            .fill(updatedRecipe)
            .clickUpdate()
            .shouldBeClosed();

        // Assert
        recipesPage.recipesList.shouldHaveRecipe(updatedRecipe);
        recipesPage.recipesList.shouldNotHaveRecipe(recipe);
    });

    it("Delete recipe -> recipe removed from list", () => {
        // Arrange
        const recipe: Recipe = {
            name: Guid.next(),
            description: "New Description"
        };
        recipesApi.addRecipe(recipe);

        // Act
        recipesPage
            .open()
            .recipesList
            .clickDelete(recipe);

        // Assert
        recipesPage.recipesList.shouldNotHaveRecipe(recipe);
    })

    it("View recipe -> recipe page opened", () => {
        // Arrange
        const recipe: Recipe = {
            name: Guid.next(),
            description: "New Description"
        };
        recipesApi.addRecipe(recipe);

        // Act
        const recipePage = recipesPage
            .open()
            .recipesList
            .clickView(recipe);

        // Assert
        recipePage.shouldBeOpened();
    });
})