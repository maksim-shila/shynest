import { ProductApiMock } from "../../../core/mocks/product-api-mock";
import { Product } from "../../../core/models/product";
import { ProductsPage } from "../../../core/sreen/product/products-page";
import { Guid } from "../../../core/utils/guid";

describe("Products Create/Update/Delete", () => {

    const productsPage = new ProductsPage();
    const productApi = new ProductApiMock();

    beforeEach("Mock Products API", () => {
        productApi.setUp();
    });

    it("Add product -> new product created", () => {
        // Arrange
        const product: Product = {
            name: Guid.next(),
            description: "New Description",
            carbohydrates: 1,
            fats: 2,
            proteins: 3,
            nutrition: 4
        };

        // Act
        productsPage
            .open()
            .clickAddProduct()
            .fill(product)
            .clickAdd()
            .shouldBeClosed();

        // Assert
        productsPage.productsList.shouldHaveProduct(product);
    });

    it("Edit product -> product info changed", () => {
        // Arrange
        const productName = Guid.next();
        const product: Product = {
            name: productName,
            description: "New Description",
            carbohydrates: 1,
            fats: 2,
            proteins: 3,
            nutrition: 4
        };
        const updatedProduct: Product = {
            name: productName + " Updated",
            description: "Updated Description",
            carbohydrates: 2,
            fats: 3,
            proteins: 4,
            nutrition: 5
        }
        productApi.addProduct(product);

        // Act
        productsPage
            .open()
            .productsList
            .clickEdit(product)
            .shouldBePrepopulated(product)
            .fill(updatedProduct)
            .clickUpdate()
            .shouldBeClosed();

        // Assert
        productsPage.productsList.shouldHaveProduct(updatedProduct);
        productsPage.productsList.shouldNotHaveProduct(product);
    });

    it("Delete product -> product removed from list", () => {
        // Arrange
        const product: Product = {
            name: Guid.next(),
            description: "New Description",
            carbohydrates: 1,
            fats: 2,
            proteins: 3,
            nutrition: 4
        };
        productApi.addProduct(product);

        // Act
        productsPage
            .open()
            .productsList
            .clickDelete(product);

        // Assert
        productsPage.productsList.shouldNotHaveProduct(product);
    })
})