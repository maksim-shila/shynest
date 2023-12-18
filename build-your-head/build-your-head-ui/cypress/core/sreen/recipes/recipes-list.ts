import { Recipe } from "../../models/recipe";
import { RecipePage } from "../recipe/recipe-page";
import { RecipeForm } from "./recipe-form";

export class RecipesList {

    public readonly productForm: RecipeForm;

    constructor() {
        this.productForm = new RecipeForm();
    }

    public clickEdit(recipe: Recipe): RecipeForm {
        const row = new RecipeRow(recipe.name);
        row.within(row => row.editBtn.click());
        return this.productForm;
    }

    public clickDelete(recipe: Recipe): RecipesList {
        const row = new RecipeRow(recipe.name);
        row.within(row => row.deleteBtn.click());
        return this;
    }

    public clickView(recipe: Recipe): RecipePage {
        const row = new RecipeRow(recipe.name);
        row.within(row => row.viewBtn.click());
        return new RecipePage(recipe);
    }

    public shouldHaveRecipe(recipe: Recipe): RecipesList {
        cy.log(`Verify table has recipe: ${recipe.name}`);
        const row = new RecipeRow(recipe.name);
        row.within(_ => cy.get("td.cell-description").should("have.text", recipe.description));
        return this;
    }

    public shouldNotHaveRecipe(recipe: Recipe): RecipesList {
        cy.log(`Verify table hasn't recipe: ${recipe.name}`);
        cy.contains(new RegExp(`^${recipe.name}$`)).should("not.exist");
        return this;
    }
}

class RecipeRow {
    private name: string;

    constructor(name: string) {
        this.name = name;
    }

    public get editBtn() {
        return cy.contains("button", /^Edit$/);
    }

    public get deleteBtn() {
        return cy.contains("button", /^Delete$/);
    }

    public get viewBtn() {
        return cy.contains("button", /^View$/);
    }

    public within(action: (row: RecipeRow) => unknown) {
        this.row().within(_ => action(this));
    }

    private row() {
        return cy.contains("td", new RegExp(`^${this.name}$`)).parent("tr");
    }
}