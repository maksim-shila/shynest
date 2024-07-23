import { Recipe } from "../models/recipe";

export class RecipeApiMock {

    private autoincrementedId = 0;
    private recipes: Recipe[] = [];

    public setUp(): void {
        cy.intercept(
            { method: "GET", url: "/api/recipe" },
            (req) => req.reply(this.recipes)
        ).as("GET /recipe");

        cy.intercept(
            { method: "GET", url: "/api/recipe/*" },
            (req) => {
                const id = Number(req.url.split("/").pop());
                const recipe = this.recipes.find(p => p.id === id);
                req.reply(recipe);
            }
        ).as("GET /recipe/{id}");

        cy.intercept(
            { method: "PUT", url: "/api/recipe" },
            (req) => {
                const recipe = this.addRecipe({ ...req.body });
                req.reply(recipe);
            }
        ).as("PUT /recipe");

        cy.intercept(
            { method: "POST", url: "/api/recipe/*" },
            (req) => {
                const id = Number(req.url.split("/").pop());
                const recipe = { id, ...req.body };
                const existing = this.recipes.find(p => p.id === recipe.id);
                const index = this.recipes.indexOf(existing);
                this.recipes[index] = recipe;
                req.reply(recipe);
            }
        ).as("POST /recipe/{id}");

        cy.intercept(
            { method: "DELETE", url: "/api/recipe/*" },
            (req) => {
                const id = Number(req.url.split("/").pop());
                this.recipes = this.recipes.filter(p => p.id !== id);
                req.reply("");
            }
        ).as("DELETE /recipe/{id}");
    }

    public addRecipe(recipe: Recipe): Recipe {
        const created = { id: this.nextId(), ...recipe };
        this.recipes.push(created);
        return created;
    }

    private nextId(): number {
        return ++this.autoincrementedId;
    }
}