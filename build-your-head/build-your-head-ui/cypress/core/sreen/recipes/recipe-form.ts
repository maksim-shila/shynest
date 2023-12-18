import { Recipe } from "../../models/recipe";

export class RecipeForm {

    public get nameInput() {
        return cy.get("#name");
    }

    public get descriptionTextArea() {
        return cy.get("#description");
    }

    public get addButton() {
        return cy.contains("button", /^Add$/);
    }

    public get updateButton() {
        return cy.contains("button", /^Update$/);
    }

    public clickAdd(): RecipeForm {
        this.addButton.click();
        return this;
    }

    public clickUpdate(): RecipeForm {
        this.updateButton.click();
        return this;
    }

    public fill(recipe: Recipe): RecipeForm {
        this.nameInput.clear().type(recipe.name);
        this.descriptionTextArea.clear().type(recipe.description);
        return this;
    }

    public shouldBePrepopulated(recipe: Recipe): RecipeForm {
        this.nameInput.should("have.value", recipe.name);
        this.descriptionTextArea.should("have.value", recipe.description);
        return this;
    }

    public shouldBeClosed(): void {
        cy.get("#recipeModal").should("not.exist");
    }
}