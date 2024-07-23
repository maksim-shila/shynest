import { Button, List, Modal, ModalBody, ModalFooter, ModalHeader } from "reactstrap"
import { Product } from "../../../../api/models"
import React from "react";
import { ProductsSearch } from "./products-search";
import { toast } from "react-toastify";

interface Props {
    isOpen: boolean,
    existingProducts: Product[]
    toggle: () => unknown,
    onSubmit: (products: Product[]) => unknown
}

export const AddProductsModal: React.FC<Props> = ({ isOpen, existingProducts, toggle, onSubmit }) => {

    const [products, setProducts] = React.useState<Product[]>([])

    const addProduct = (product: Product) => {
        if (existingProducts.some(p => p.id === product.id) || products.some(p => p.id === product.id)) {
            toast.warn(`Product '${product.name}' already assigned to recipe`);
            return;
        }
        setProducts(prev => {
            const newProducts = [...prev];
            newProducts.push(product);
            return newProducts;
        })
    }

    const handleAddProductsClick = () => {
        onSubmit(products);
        setProducts([]);
    }

    return (
        <Modal id="productModal" isOpen={isOpen} toggle={toggle} size="lg">
            <ModalHeader toggle={toggle}>Add Products</ModalHeader>
            <ModalBody>
                <ProductsSearch onAdd={addProduct} />
                <hr />
                <List>
                    {products.map(p => (
                        <li key={p.id}>{p.name}</li>
                    ))}
                </List>
            </ModalBody>
            <ModalFooter>
                <Button color="success" onClick={handleAddProductsClick}>Add Products</Button>
            </ModalFooter>
        </Modal>
    )
}