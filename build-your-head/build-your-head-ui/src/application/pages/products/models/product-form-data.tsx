export interface ProductFormData {
    name: string,
    description: string,
    proteins: number,
    carbohydrates: number,
    fats: number,
    nutrition: number,
    imageBase64: string | null,
    imageChanged: boolean
}