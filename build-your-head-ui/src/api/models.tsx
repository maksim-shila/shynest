export interface Product {
    id?: number,
    name: string,
    description?: string,
    proteins: number,
    carbohydrates: number,
    fats: number,
    nutrition: number
}

export interface Recipe {
    id?: number,
    name: string,
    description?: string
}

export interface AttachImageRequest {
    productId: number,
    imagePath: string,
    primary: boolean
}