import { ApiCall, ApiClientBase } from "./core";
import { AttachImageRequest, Recipe, Product } from "./models";

export interface IApiClient {
    Recipe: {
        get: (id: number) => ApiCall<Recipe>,
        getAll: () => ApiCall<Recipe[]>,
        put: (recipe: Recipe) => ApiCall<Recipe>,
        post: (id: number, recipe: Recipe) => ApiCall<Recipe>,
        delete: (id: number) => ApiCall<string>,
        Product: {
            getAll: (recipeId: number) => ApiCall<Product[]>,
            put: (recipeId: number, products: Product[]) => ApiCall<string>,
            delete: (recipeId: number, product: Product) => ApiCall<string>
        }
    },
    Product: {
        get: (id: number) => ApiCall<Product>,
        getAll: () => ApiCall<Product[]>,
        put: (product: Product) => ApiCall<Product>,
        post: (id: number, product: Product) => ApiCall<Product>,
        delete: (id: number) => ApiCall<string>,
        attachImage: (request: AttachImageRequest) => ApiCall<string>,
        getPrimaryImage: (productId: number) => ApiCall<string>
    },
    Image: {
        post: (imageBase64: string) => ApiCall<string>
    }
}

export class ApiClient extends ApiClientBase implements IApiClient {

    public Recipe = {
        get: (id: number) => this.get<Recipe>(`/recipe/${id}`),
        getAll: () => this.get<Recipe[]>("/recipe"),
        put: (recipe: Recipe) => this.put<Recipe>("/recipe", recipe),
        post: (id: number, recipe: Recipe) => this.post<Recipe>(`/recipe/${id}`, recipe),
        delete: (id: number) => this.delete<string>(`/recipe/${id}`),
        Product: {
            getAll: (recipeId: number) => this.get<Product[]>(`/recipe/${recipeId}/product`),
            put: (recipeId: number, products: Product[]) => this.put<string>(`/recipe/${recipeId}/product`, { productsIds: products.map(p => p.id) }),
            delete: (recipeId: number, product: Product) => this.delete<string>(`/recipe/${recipeId}/product/${product.id}`)
        }
    }

    public Product = {
        get: (id: number) => this.get<Product>(`/product/${id}`),
        getAll: () => this.get<Product[]>("/product"),
        put: (product: Product) => this.put<Product>("/product", product),
        post: (id: number, product: Product) => this.post<Product>(`/product/${id}`, product),
        delete: (id: number) => this.delete<string>(`/product/${id}`),
        attachImage: (request: AttachImageRequest) => {
            const url = `/product/${request.productId}/image`;
            const body = { imagePath: request.imagePath, primary: request.primary };
            return this.post<string>(url, body);
        },
        getPrimaryImage: (productId: number) => this.get<string>(`/product/${productId}/image/primary`)
    }

    public Image = {
        post: (imageBase64: string) => this.post<string>("/image", { imageBase64 })
    }
}

