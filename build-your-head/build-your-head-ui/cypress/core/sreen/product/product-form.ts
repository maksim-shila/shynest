import { Product } from "../../models/product";

export class ProductForm {

    public get addButton() {
        return cy.contains("button", /^Add$/);
    }

    public get updateButton() {
        return cy.contains("button", /^Update$/);
    }

    public get nameInput() {
        return cy.get("#name");
    }

    public get descriptionTextArea() {
        return cy.get("#description");
    }

    public get carbohydratesInput() {
        return cy.get("#carbohydrates");
    }

    public get fatsInput() {
        return cy.get("#fats");
    }

    public get proteinsInput() {
        return cy.get("#proteins");
    }

    public get nutritionInput() {
        return cy.get("#nutrition");
    }

    public clickAdd(): ProductForm {
        this.addButton.click();
        return this;
    }

    public clickUpdate(): ProductForm {
        this.updateButton.click();
        return this;
    }

    public fill(product: Product): ProductForm {
        this.nameInput.clear().type(product.name);
        this.descriptionTextArea.clear().type(product.description);
        this.carbohydratesInput.invoke('val', '').clear().type(`${product.carbohydrates}`);
        this.fatsInput.clear().invoke('val', '').type(`${product.fats}`);
        this.proteinsInput.clear().invoke('val', '').type(`${product.proteins}`);
        this.nutritionInput.clear().invoke('val', '').type(`${product.nutrition}`);
        return this;
    }

    public shouldBePrepopulated(product: Product): ProductForm {
        this.nameInput.should("have.value", product.name);
        this.descriptionTextArea.should("have.value", product.description);
        this.carbohydratesInput.should("have.value", product.carbohydrates);
        this.fatsInput.should("have.value", product.fats);
        this.proteinsInput.should("have.value", product.proteins);
        this.nutritionInput.should("have.value", product.nutrition);
        return this;
    }

    public shouldBeClosed(): void {
        cy.get("#productModal").should("not.exist");
    }
}