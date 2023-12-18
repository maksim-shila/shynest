import { Product } from "../models/product";

export class ProductApiMock {

    private autoincrementedId = 0;
    private products: Product[] = [];

    public setUp(): void {
        cy.intercept(
            { method: "GET", url: "/api/product" },
            (req) => req.reply(this.products)
        ).as("GET /product");

        cy.intercept(
            { method: "PUT", url: "/api/product" },
            (req) => {
                const product = this.addProduct({ ...req.body });
                req.reply(product);
            }
        ).as("PUT /product");

        cy.intercept(
            { method: "GET", url: "/api/product/*/image/primary" },
            (req) => {
                req.reply({ body: null });
            }
        ).as("GET /product/*/image/primary");

        cy.intercept(
            { method: "POST", url: "/api/product/*" },
            (req) => {
                const id = Number(req.url.split("/").pop());
                const product = { id, ...req.body };
                const existing = this.products.find(p => p.id === product.id);
                const index = this.products.indexOf(existing);
                this.products[index] = product;
                req.reply(product);
            }
        ).as("POST /product/{id}");

        cy.intercept(
            { method: "DELETE", url: "/api/product/*" },
            (req) => {
                const id = Number(req.url.split("/").pop());
                this.products = this.products.filter(p => p.id !== id);
                req.reply("");
            }
        ).as("DELETE /product/{id}");
    }

    public addProduct(product: Product): Product {
        const created = { id: this.nextId(), ...product };
        this.products.push(created);
        return created;
    }

    private nextId(): number {
        return ++this.autoincrementedId;
    }
}