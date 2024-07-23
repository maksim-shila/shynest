import { Recipe } from "../../models/recipe";

export class RecipePage {

    private recipe: Recipe;

    constructor(recipe: Recipe) {
        this.recipe = recipe;
    }

    public shouldBeOpened(): RecipePage {
        cy.url().should("contain", "/recipe/");
        cy.contains(this.recipe.name).should("be.visible");
        return this;
    }
}