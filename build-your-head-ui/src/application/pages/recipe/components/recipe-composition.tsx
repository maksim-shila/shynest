import React from "react";
import { Product, Recipe } from "../../../../api/models";
import { Button, ListGroup, ListGroupItem } from "reactstrap";
import { GlobalContext } from "../../../context/global-context";
import { AddProductsModal } from "./add-products-modal";
import { toast } from "react-toastify";
import { useLoader } from "../../../../hooks/loader";

interface Props {
    recipe: Recipe
}

export const RecipeComposition: React.FC<Props> = ({ recipe }) => {

    const { $api } = React.useContext(GlobalContext);

    const [products, setProducts] = React.useState<Product[]>([]);
    const [isAddProductsDialogShown, setIsAddProductsDialogShown] = React.useState(false);

    React.useEffect(() => {
        fetchProducts();
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const fetchProducts = useLoader(async () => {
        if (!recipe.id || recipe.id < 0) {
            toast.error("Incorrect recipe ID");
            return;
        }
        const response = await $api().Recipe.Product.getAll(recipe.id).invoke();
        if (response.success && response.data) {
            setProducts(response.data);
        }
    })

    const handleAddProductClick = () => {
        setIsAddProductsDialogShown(true)
    }

    const handleAddProductsSubmit = useLoader(async (products: Product[]) => {
        if (!recipe.id || recipe.id < 0) {
            toast.error("Incorrect recipe ID");
            return;
        }
        const request = $api().Recipe.Product.put(recipe.id, products);
        const response = await request.invoke();
        if (response.success) {
            toast.success(response.data);
            setIsAddProductsDialogShown(false);
            await fetchProducts();
        }
    })

    const handleDeleteClick = useLoader(async (product: Product) => {
        if (!recipe.id || recipe.id < 0) {
            toast.error("Incorrect recipe ID");
            return;
        }
        const request = $api().Recipe.Product.delete(recipe.id, product);
        const response = await request.invoke();
        if (response.success) {
            toast.success(response.data);
            await fetchProducts();
        }
    })

    return (
        <React.Fragment>
            <h2>Composition</h2>
            <ListGroup flush={true}>
                {products && products.map(product => (
                    <ListGroupItem key={product.id}>
                        <span>{product.name}</span>
                        <Button
                            color="danger"
                            className="float-end"
                            onClick={() => handleDeleteClick(product)}
                        >
                            Delete
                        </Button>
                    </ListGroupItem>
                ))}
            </ListGroup>
            <Button onClick={handleAddProductClick}>Add Product</Button>
            <AddProductsModal
                isOpen={isAddProductsDialogShown}
                existingProducts={products}
                toggle={() => setIsAddProductsDialogShown(prev => !prev)}
                onSubmit={handleAddProductsSubmit}
            />
        </React.Fragment>
    )
}