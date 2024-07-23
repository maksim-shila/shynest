import { RecipeForm } from "./recipe-form";
import { RecipesList } from "./recipes-list";

export class RecipesPage {

    public readonly recipeForm: RecipeForm;
    public readonly recipesList: RecipesList;

    constructor() {
        this.recipeForm = new RecipeForm();
        this.recipesList = new RecipesList();
    }

    public get addRecipeButton() {
        return cy.contains("button", /^Add Recipe$/);
    }

    public open(): RecipesPage {
        cy.visit("/recipes");
        return this;
    }

    public clickAddRecipe(): RecipeForm {
        this.addRecipeButton.click();
        return this.recipeForm;
    }
}