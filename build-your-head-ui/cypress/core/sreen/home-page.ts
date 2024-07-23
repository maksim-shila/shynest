export class HomePage {

    public open(): HomePage {
        cy.visit("/");
        return this;
    }
}