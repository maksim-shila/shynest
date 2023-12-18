import React from "react"
import { Button } from "reactstrap";
import { Product } from "../../../api/models";
import { useLoader } from "../../../hooks/loader";
import { GlobalContext } from "../../context/global-context";
import { ProductViewModal } from "./components/product-view-modal";
import { ProductsList } from "./components/products-list"
import { ProductFormData } from "./models/product-form-data";
import { useAuth } from "oidc-react";

export const ProductsPage: React.FC = () => {

    const auth = useAuth()
    const { $api } = React.useContext(GlobalContext);

    const [products, setProducts] = React.useState<Product[]>([]);

    const [isProductViewModalOpen, setIsProductViewModalOpen] = React.useState(false);
    const [activeProduct, setActiveProduct] = React.useState<Product | null>(null);

    React.useEffect(() => {
        document.title = "Products";
        fetchProducts();
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [auth]);

    const fetchProducts = useLoader(async (): Promise<void> => {
        const response = await $api().Product.getAll().invoke();
        const products = response.data;
        if (!response.success || products == null) {
            return;
        }
        setProducts(products);
    })

    const showProductViewModal = (product: Product | null) => {
        setActiveProduct(product);
        setIsProductViewModalOpen(true);
    }

    const toggleProductViewModal = () => {
        if (isProductViewModalOpen) {
            setActiveProduct(null);
        }
        setIsProductViewModalOpen(v => !v);
    }

    const handleSubmit = useLoader(async (data: ProductFormData) => {
        const isEdit = activeProduct !== null;
        if (isEdit && !activeProduct.id) {
            console.error("Edited product hasn't id");
            return;
        }

        const { imageBase64, imageChanged, ...product } = data;
        const request = isEdit ? $api().Product.post(activeProduct.id!, product) : $api().Product.put(product);
        const response = await request.invoke();
        const productId = response.data?.id;
        if (!productId) {
            console.error(`Failed to ${isEdit ? "update" : "create"} product`);
            return;
        }
        if (imageChanged && imageBase64) {
            await postImage(productId, imageBase64);
        }

        setIsProductViewModalOpen(false);
        setActiveProduct(null);
        await fetchProducts();
    })

    const postImage = async (productId: number, imageBase64: string): Promise<void> => {
        const response = await $api().Image.post(imageBase64).invoke();
        const imagePath = response.data;
        if (!response.success || !imagePath) {
            return;
        }

        await $api().Product.attachImage({ productId, imagePath, primary: true }).invoke();
    }

    const deleteProduct = async (product: Product) => {
        if (!product.id) {
            console.error("Couldn't delete product without id");
            return;
        }
        const request = $api().Product.delete(product.id)
        const response = await request.invoke();
        if (!response.success) {
            return;
        }
        await fetchProducts();
    }

    return (
        <React.Fragment>
            <Button onClick={() => showProductViewModal(null)}>Add Product</Button>
            <ProductsList
                products={products}
                onEdit={showProductViewModal}
                onDelete={deleteProduct}
            />
            <ProductViewModal
                isOpen={isProductViewModalOpen}
                toggle={toggleProductViewModal}
                product={activeProduct}
                onSubmit={handleSubmit}
            />
        </React.Fragment>
    )
}