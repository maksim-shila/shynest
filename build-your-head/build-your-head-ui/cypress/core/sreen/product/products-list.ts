import { Product } from "../models/product";
import { ProductForm } from "./product-form";

export class ProductsList {

    public readonly productForm: ProductForm;

    constructor() {
        this.productForm = new ProductForm();
    }

    public clickEdit(product: Product): ProductForm {
        this.row(product.name).within(_ => {
            cy.contains("button", /^Edit$/).click();
        });

        return this.productForm;
    }

    public clickDelete(product: Product) {
        this.row(product.name).within(_ => {
            cy.contains("button", /^Delete$/).click();
        });
    }

    public shouldHaveProduct(product: Product): ProductsList {
        cy.log(`Verify table has product: ${product.name}`);
        this.row(product.name).within(_ => {
            cy.get("td.cell-description").should("have.text", product.description);
            cy.get("td.cell-carbohydrates").should("have.text", product.carbohydrates);
            cy.get("td.cell-proteins").should("have.text", product.proteins);
            cy.get("td.cell-fats").should("have.text", product.fats);
            cy.get("td.cell-nutrition").should("have.text", product.nutrition);
        });

        return this;
    }

    public shouldNotHaveProduct(product: Product): ProductsList {
        cy.log(`Verify table hasn't product: ${product.name}`);
        cy.contains(new RegExp(`^${product.name}$`)).should("not.exist");
        return this;
    }

    private row(productName: string) {
        return cy.contains("td", new RegExp(`^${productName}$`)).parent("tr");
    }
}